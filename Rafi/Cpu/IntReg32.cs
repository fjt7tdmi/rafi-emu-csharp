using System;

namespace Rafi
{
    // Accessor to integer registers for RV32
    internal class IntReg32
    {
        private readonly IntReg64 intReg;

        public IntReg32(IntReg64 intReg)
        {
            this.intReg = intReg;
        }

        public uint this[int index]
        {
            get => (uint)intReg[index];
            set => intReg[index] = value;
        }
    }
}
