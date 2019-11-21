using System;

namespace Rafi
{
    // Accessor to CSR for RV32
    internal class Csr32
    {
        private readonly Csr64 csr64;

        public Csr32(Csr64 csr64)
        {
            this.csr64 = csr64;
        }

        public uint this[int index]
        {
            get => (uint)csr64[index];
            set => csr64[index] = value;
        }
    }
}
