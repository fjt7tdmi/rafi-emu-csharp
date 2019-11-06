using System;

namespace Rafi
{
    public class Utils
    {
        public static uint Pick(uint insn, int lsb, int width = 1) => (insn >> lsb) & ((1u << width) - 1);

        public static uint SignExtend(int width, byte value) => SignExtend(width, (uint)value);

        public static uint SignExtend(int width, ushort value) => SignExtend(width, (uint)value);

        public static uint SignExtend(int width, uint value)
        {
            var sign = (value >> (width - 1)) & 1;
            var mask = (1u << width) - 1;

            if (sign == 0)
            {
                return value & mask;
            }
            else
            {
                return value | (~mask);
            }
        }

        public static uint ZeroExtend(int width, byte value) => ZeroExtend(width, (uint)value);

        public static uint ZeroExtend(int width, ushort value) => ZeroExtend(width, (uint)value);

        public static uint ZeroExtend(int width, uint value)
        {
            var mask = (1u << width) - 1;

            return value & mask;
        }
    }
}
