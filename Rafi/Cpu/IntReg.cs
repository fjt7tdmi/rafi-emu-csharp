using System;

namespace Rafi
{
    internal class IntReg
    {
        private readonly uint[] values = new uint[32];

        public uint[] Values => values;

        public IntReg()
        {
        }

        public IntReg(IntReg intReg) 
        {
            Buffer.BlockCopy(intReg.values, 0, values, 0, 32);
        }

        public uint this[int index]
        {
            get => values[index];
            set
            {
                if (index != 0)
                {
                    values[index] = value;
                }
            }
        }
    }
}
