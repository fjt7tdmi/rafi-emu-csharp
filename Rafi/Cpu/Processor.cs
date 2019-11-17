using System;

namespace Rafi
{
    internal class Processor
    {
        private readonly Decoder decoder = new Decoder();
        private readonly Logger logger = new Logger("cpu");

        private readonly Bus bus;

        internal Core Core { get; }

        public Processor(Bus bus)
        {
            this.bus = bus;

            Core = new Core(bus);
        }

        public void ProcessCycle()
        {
            var insn = bus.ReadUInt32(Core.Pc);

            try
            {
                var op = decoder.Decode(insn);

                logger.Trace($"{Core.Pc:x} {op}");

                Core.NextPc = Core.Pc + 4;

                op.Execute(Core);

                var trap = op.PostCheckTrap(Core);
                if (trap != null)
                {
                    ProcessTrap(trap);
                    return;
                }
            }
            finally
            {
                Core.Pc = Core.NextPc;
            }
        }

        private void ProcessTrap(Trap trap)
        {
            switch (trap.TrapType)
            {
                case TrapType.Exception:
                    ProcessException(trap);
                    break;
                case TrapType.Return:
                    ProcessTrapReturn(trap);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void ProcessException(Trap trap)
        {
            var mtvec = Core.Csr.Read<MTVEC>();
            var mstatus = Core.Csr.Read<MSTATUS>();

            mstatus.MPIE = mstatus.MIE;
            mstatus.MIE = 0;
            mstatus.MPP = 3; // PrivilegeLevel.Machine

            Core.Csr.Write(mstatus);
            Core.Csr.Write(new MCAUSE(trap.Cause));
            Core.Csr.Write(new MEPC(trap.Pc));
            Core.Csr.Write(new MTVAL(trap.Value));

            Core.NextPc = mtvec.BASE * 4;
        }

        private void ProcessTrapReturn(Trap trap)
        {
            var mstatus = Core.Csr.Read<MSTATUS>();
            var mepc = Core.Csr.Read<MEPC>();

            mstatus.MPP = 0;
            mstatus.MIE = mstatus.MPIE;

            Core.Csr.Write(mstatus);

            Core.NextPc = mepc.Value;
        }
    }
}
