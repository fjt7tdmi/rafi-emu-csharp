using System;

namespace Rafi
{
    internal class BitField
    {
        public uint Value { get; set; }

        public BitField(uint value)
        {
            Value = value;
        }

        public uint Get(int msb) => Get(msb, msb);

        public uint Get(int msb, int lsb)
        {
            var width = msb - lsb + 1;
            var mask = ((1u << width) - 1u) << lsb;

            return (Value & mask) >> lsb;
        }

        public void Set(int msb, uint value) => Set(msb, msb, value);

        public void Set(int msb, int lsb, uint value)
        {
            var width = msb - lsb + 1;
            var mask = ((1u << width) - 1u) << lsb;

            Value = (Value & ~mask) | ((value << lsb) & mask);
        }
    }
}
