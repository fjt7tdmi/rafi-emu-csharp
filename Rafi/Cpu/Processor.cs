using System;

namespace Rafi
{
    internal class Processor
    {
        private readonly Decoder decoder = new Decoder();

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

            var op = decoder.Decode(insn);

            Core.NextPc = Core.Pc + 4;

            op.Execute(Core);

            Core.Pc = Core.NextPc;
        }
    }
}
