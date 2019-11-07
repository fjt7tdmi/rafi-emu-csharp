using System;

namespace Rafi
{
    internal class Core
    {
        public Bus Bus { get; }

        public Csr Csr { get; } = new Csr();

        public FpReg FpReg { get; } = new FpReg();

        public IntReg IntReg { get; } = new IntReg();

        public uint Pc { get; set; } = 0;

        public uint NextPc { get; set; } = 0;

        public Core(Bus bus)
        {
            Bus = bus;
        }
    }
}
