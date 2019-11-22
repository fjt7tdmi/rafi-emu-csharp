using System;

namespace Rafi
{
    internal class Processor
    {
        private readonly Logger logger = new Logger("cpu");

        private readonly Bus bus;
        private readonly IDecoder decoder;

        internal Core Core { get; }

        public Processor(int xlen, Bus bus)
        {
            this.bus = bus;

            switch (xlen)
            {
                case 32:
                    decoder = new Decoder32();
                    break;
                case 64:
                    decoder = new Decoder64();
                    break;
                default:
                    throw new NotImplementedException();
            }

            Core = new Core(bus);
        }

        public void ProcessCycle()
        {
            var insn = bus.ReadUInt32(Core.Pc32);

            try
            {
                var op = decoder.Decode(insn);

                logger.Trace($"{Core.Pc32:x} {op}");

                Core.NextPc32 = Core.Pc32 + 4;

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
                Core.Pc32 = Core.NextPc32;
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
            var mtvec = Core.Csr64.Read<MTVEC>();
            var mstatus = Core.Csr64.Read<MSTATUS>();

            mstatus.MPIE = mstatus.MIE;
            mstatus.MIE = 0;
            mstatus.MPP = 3; // PrivilegeLevel.Machine

            Core.Csr64.Write(mstatus);
            Core.Csr64.Write(new MCAUSE(trap.Cause));
            Core.Csr64.Write(new MEPC(trap.Pc));
            Core.Csr64.Write(new MTVAL(trap.Value));

            Core.NextPc64 = mtvec.BASE * 4;
        }

        private void ProcessTrapReturn(Trap trap)
        {
            var mstatus = Core.Csr64.Read<MSTATUS>();
            var mepc = Core.Csr64.Read<MEPC>();

            mstatus.MPP = 0;
            mstatus.MIE = mstatus.MPIE;

            Core.Csr64.Write(mstatus);

            Core.NextPc64 = mepc.Value;
        }
    }
}
