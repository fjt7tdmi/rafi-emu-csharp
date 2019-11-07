using System;

namespace Rafi
{
    internal class Csr
    {
        private readonly uint[] values = new uint[0x1000];

        public uint this[int index]
        {
            get => values[index];
            set => values[index] = value;
        }
    }
}
