using System;

namespace Rafi
{
    internal class Core
    {
        public Bus Bus { get; }

        public Csr64 Csr64 { get; }
        public Csr32 Csr32 { get; }

        public FpReg FpReg { get; } = new FpReg();

        public IntReg64 IntReg64 { get; }
        public IntReg32 IntReg32 { get; }

        public uint NextPc32
        {
            get => (uint)NextPc64;
            set => NextPc64 = value;
        }

        public ulong NextPc64 { get; set; }

        public uint Pc32
        {
            get => (uint)Pc64;
            set => Pc64 = value;
        }

        public ulong Pc64 { get; set; }

        public Core(Bus bus)
        {
            Bus = bus;
            Csr64 = new Csr64();
            Csr32 = new Csr32(Csr64);
            IntReg64 = new IntReg64();
            IntReg32 = new IntReg32(IntReg64);
        }
    }
}
