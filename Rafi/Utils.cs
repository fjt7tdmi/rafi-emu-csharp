using System;

namespace Rafi
{
    public class Utils
    {
        public static uint Pick(uint insn, int lsb, int width = 1) => (insn >> lsb) & ((1u << width) - 1);

        public static uint SignExtend32(int width, byte value) => SignExtend32(width, (uint)value);

        public static uint SignExtend32(int width, ushort value) => SignExtend32(width, (uint)value);

        public static uint SignExtend32(int width, uint value)
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

        public static ulong SignExtend64(int width, byte value) => SignExtend64(width, (ulong)value);

        public static ulong SignExtend64(int width, ushort value) => SignExtend64(width, (ulong)value);

        public static ulong SignExtend64(int width, uint value) => SignExtend64(width, (ulong)value);

        public static ulong SignExtend64(int width, ulong value)
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

        public static uint ZeroExtend32(int width, byte value) => ZeroExtend32(width, (uint)value);

        public static uint ZeroExtend32(int width, ushort value) => ZeroExtend32(width, (uint)value);

        public static uint ZeroExtend32(int width, uint value)
        {
            var mask = (1u << width) - 1;

            return value & mask;
        }

        public static ulong ZeroExtend64(int width, byte value) => ZeroExtend64(width, (ulong)value);

        public static ulong ZeroExtend64(int width, ushort value) => ZeroExtend64(width, (ulong)value);

        public static ulong ZeroExtend64(int width, uint value) => ZeroExtend64(width, (ulong)value);

        public static ulong ZeroExtend64(int width, ulong value)
        {
            var mask = (1u << width) - 1;

            return value & mask;
        }
    }
}
