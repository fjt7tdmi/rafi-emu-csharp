using System;

namespace Rafi
{
    internal class IntReg64
    {
        private readonly ulong[] values = new ulong[32];

        public ulong[] Values => values;

        public IntReg64()
        {
        }

        public ulong this[int index]
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
