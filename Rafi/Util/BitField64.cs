using System;

namespace Rafi
{
    internal class BitField64
    {
        public ulong Value { get; set; }

        public BitField64(ulong value)
        {
            Value = value;
        }

        public ulong Get(int msb) => Get(msb, msb);

        public ulong Get(int msb, int lsb)
        {
            var width = msb - lsb + 1;
            var mask = ((1ul << width) - 1ul) << lsb;

            return (Value & mask) >> lsb;
        }

        public void Set(int msb, ulong value) => Set(msb, msb, value);

        public void Set(int msb, int lsb, ulong value)
        {
            var width = msb - lsb + 1;
            var mask = ((1ul << width) - 1ul) << lsb;

            Value = (Value & ~mask) | ((value << lsb) & mask);
        }
    }
}
