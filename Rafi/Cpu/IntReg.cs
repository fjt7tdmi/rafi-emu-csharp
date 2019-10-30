using System;

namespace Rafi
{
    internal class IntReg
    {
        private readonly uint[] values = new uint[32];

        public uint[] Values
        {
            get
            {
                var array = new uint[32];
                Array.Copy(values, 0, array, 0, 32);
                return array;
            }
        }

        public uint Get(int index) => values[index];

        public void Set(int index, uint value)
        {
            if (index != 0)
            {
                values[index] = value;
            }
        }
    }
}
