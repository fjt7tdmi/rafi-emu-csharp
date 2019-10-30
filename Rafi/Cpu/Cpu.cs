using System;

namespace Rafi
{
    internal class Cpu
    {
        public FpReg FpReg { get; } = new FpReg();

        public IntReg IntReg { get; } = new IntReg();

        public uint Pc { get; } = 0;
    }
}
