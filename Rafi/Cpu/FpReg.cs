using System;

namespace Rafi
{
    internal class FpReg
    {
        private readonly ulong[] values = new ulong[32];

        public ulong[] Values
        {
            get
            {
                var array = new ulong[32];
                Array.Copy(values, 0, array, 0, 32);
                return array;
            }
        }

        public ulong Get(int index) => values[index];

        public void Set(int index, ulong value) => values[index] = value;
    }
}
