using System;

namespace Rafi
{
    internal class Decoder
    {
        public IOp Decode(uint insn) => DecodeRV32I(insn);
        
        public IOp DecodeRV32I(uint insn)
        {
            var opcode = Pick(insn, 0, 7);

            var operandR = DecodeOperandR(insn);
            var operandI = DecodeOperandI(insn);

            switch (opcode)
            {
                case 0b0110111:
                    return new RV32I.LUI(operandI.rd, operandI.imm);
                default:
                    throw new FormatException($"Unknown operand 0x{insn:x}");
            }
        }

        private (int funct7, int rs2, int rs1, int funct3, int rd) DecodeOperandR(uint insn) =>
            new ValueTuple<int, int, int, int, int>(
                (int)Pick(insn, 25, 7),
                (int)Pick(insn, 20, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3),
                (int)Pick(insn, 7, 5));

        private (uint imm, int rs1, int funct3, int rd) DecodeOperandI(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Pick(insn, 20, 12),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3),
                (int)Pick(insn, 7, 5));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandS(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Pick(insn, 25, 7) << 5 | Pick(insn, 7, 5),
                (int)Pick(insn, 20, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandB(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Pick(insn, 31) << 12 | Pick(insn, 7) << 11 | Pick(insn, 25, 6) << 5 | Pick(insn, 8, 4) << 1,
                (int)Pick(insn, 20, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3));

        private (uint imm, int rd) DecodeOperandU(uint insn)
            => new ValueTuple<uint, int>(
                Pick(insn, 12, 20) << 12,
                (int)Pick(insn, 7, 5));

        private (uint imm, int rd) DecodeOperandJ(uint insn) =>
            new ValueTuple<uint, int>(
                SignExtend(21,
                    Pick(insn, 31) << 20 |
                    Pick(insn, 21, 10) << 1 |
                    Pick(insn, 20) << 11 |
                    Pick(insn, 12, 8) << 12
                ),
                (int)Pick(insn, 7, 5));

        private uint Pick(uint insn, int lsb, int width = 1) =>
            (insn >> lsb) & ((1u << width) - 1);

        private uint SignExtend(int width, uint value)
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
    }
}
