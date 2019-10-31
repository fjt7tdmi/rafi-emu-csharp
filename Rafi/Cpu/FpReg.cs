using System;

namespace Rafi
{
    internal class FpReg
    {
        private readonly uint[] values = new uint[32];

        public uint[] Values => values;

        public FpReg()
        {
        }

        public FpReg(FpReg fpReg)
        {
            Buffer.BlockCopy(fpReg.values, 0, values, 0, 32);
        }

        public uint this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }
    }
}
