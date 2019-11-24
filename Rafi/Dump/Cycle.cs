using System;

namespace Rafi
{
    internal class Cycle
    {
        public ulong Pc { get; }
        public uint Insn { get; }
        public Op Op { get; }
        public Trap Trap { get; }

        public Cycle(ulong pc, uint insn, Op op = null, Trap trap = null)
        {
            Pc = pc;
            Insn = insn;
            Op = op;
            Trap = trap;
        }
    }
}
