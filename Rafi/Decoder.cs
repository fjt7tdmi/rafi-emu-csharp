using System;

namespace Rafi
{
    internal class Decoder
    {
        public IOp Decode(uint insn) => DecodeRV32I(insn);
        
        public IOp DecodeRV32I(uint insn)
        {
            var opcode = Pick(insn, 0, 7);

            var r = DecodeOperandR(insn);
            var i = DecodeOperandI(insn);
            var i_csr = DecodeOperandI_CSR(insn);
            var s = DecodeOperandS(insn);
            var b = DecodeOperandB(insn);
            var u = DecodeOperandU(insn);
            var j = DecodeOperandJ(insn);

            switch (opcode)
            {
                case 0b0110111:
                    return new RV32I.LUI(u.rd, u.imm);
                case 0b0010111:
                    return new RV32I.AUIPC(u.rd, u.imm);
                case 0b1101111:
                    return new RV32I.JAL(j.rd, j.imm);
                case 0b1100111:
                    return new RV32I.JALR(i.rd, i.rs1, i.imm);
                case 0b1100011:
                    switch(b.funct3)
                    {
                        case 0b000:
                            return new RV32I.BEQ(b.rs1, b.rs2, b.imm);
                        case 0b001:
                            return new RV32I.BNE(b.rs1, b.rs2, b.imm);
                        case 0b100:
                            return new RV32I.BLT(b.rs1, b.rs2, b.imm);
                        case 0b101:
                            return new RV32I.BGE(b.rs1, b.rs2, b.imm);
                        case 0b110:
                            return new RV32I.BLTU(b.rs1, b.rs2, b.imm);
                        case 0b111:
                            return new RV32I.BGEU(b.rs1, b.rs2, b.imm);
                        default:
                            break;
                    }
                    break;
                case 0b0000011:
                    switch (i.funct3)
                    {
                        case 0b000:
                            return new RV32I.LB(i.rd, i.rs1, i.imm);
                        case 0b001:
                            return new RV32I.LH(i.rd, i.rs1, i.imm);
                        case 0b010:
                            return new RV32I.LW(i.rd, i.rs1, i.imm);
                        case 0b100:
                            return new RV32I.LBU(i.rd, i.rs1, i.imm);
                        case 0b101:
                            return new RV32I.LHU(i.rd, i.rs1, i.imm);
                        default:
                            break;
                    }
                    break;
                case 0b0100011:
                    switch (s.funct3)
                    {
                        case 0b000:
                            return new RV32I.SB(s.rs1, s.rs2, s.imm);
                        case 0b001:
                            return new RV32I.SH(s.rs1, s.rs2, s.imm);
                        case 0b010:
                            return new RV32I.SW(s.rs1, s.rs2, s.imm);
                        default:
                            break;
                    }
                    break;
                case 0b0010011:
                    switch (i.funct3)
                    {
                        case 0b000:
                            return new RV32I.ADDI(i.rd, i.rs1, i.imm);
                        case 0b010:
                            return new RV32I.SLTI(i.rd, i.rs1, i.imm);
                        case 0b011:
                            return new RV32I.SLTIU(i.rd, i.rs1, i.imm);
                        case 0b100:
                            return new RV32I.XORI(i.rd, i.rs1, i.imm);
                        case 0b110:
                            return new RV32I.ORI(i.rd, i.rs1, i.imm);
                        case 0b111:
                            return new RV32I.ANDI(i.rd, i.rs1, i.imm);
                        default:
                            break;
                    }
                    break;
                case 0b0110011:                    
                    if (r.funct3 == 0b001 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SLLI(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SRLI(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0100000)
                    {
                        return new RV32I.SRAI(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b000 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.ADD(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b000 && r.funct7 == 0b0100000)
                    {
                        return new RV32I.SUB(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b001 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SLL(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b010 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SLT(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b011 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SLTU(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b100 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.XOR(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.SRL(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0100000)
                    {
                        return new RV32I.SRA(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b110 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.OR(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b111 && r.funct7 == 0b0000000)
                    {
                        return new RV32I.AND(r.rd, r.rs1, r.rs2);
                    }
                    else
                    {
                        break;
                    }
                case 0b0001111:
                    if (i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000 && Pick(insn, 28, 4) == 0b0000)
                    {
                        return new RV32I.FENCE((int)Pick(insn, 24, 4), (int)Pick(insn, 20, 4));
                    }
                    else if (i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000 && i.imm == 0b0000_0000_0000)
                    {
                        return new RV32I.FENCE_I();
                    }
                    else
                    {
                        break;
                    }
                case 0b1110011:
                    if (i.imm == 0b0000_0000_0000 && i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000)
                    {
                        return new RV32I.ECALL();
                    }
                    else if (i.imm == 0b0000_0000_0001 && i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000)
                    {
                        return new RV32I.EBREAK();
                    }
                    else
                    {
                        switch (i_csr.funct3)
                        {
                            case 0b001:
                                return new RV32I.CSRRW(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b010:
                                return new RV32I.CSRRS(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b011:
                                return new RV32I.CSRRC(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b101:
                                return new RV32I.CSRRWI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            case 0b110:
                                return new RV32I.CSRRSI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            case 0b111:
                                return new RV32I.CSRRCI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            default:
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }

            throw new FormatException($"Unknown insn 0x{insn:x}");
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
                SignExtend(12,
                    Pick(insn, 20, 12)),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3),
                (int)Pick(insn, 7, 5));

        private (int csr, uint zimm, int rs1, int funct3, int rd) DecodeOperandI_CSR(uint insn) =>
            new ValueTuple<int, uint, int, int, int>(
                (int)Pick(insn, 20, 12),
                Pick(insn, 15, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3),
                (int)Pick(insn, 7, 5));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandS(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                SignExtend(12,
                    Pick(insn, 25, 7) << 5 |
                    Pick(insn, 7, 5)),
                (int)Pick(insn, 20, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandB(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                SignExtend(13,
                    Pick(insn, 31) << 12 |
                    Pick(insn, 7) << 11 |
                    Pick(insn, 25, 6) << 5 |
                    Pick(insn, 8, 4) << 1),
                (int)Pick(insn, 20, 5),
                (int)Pick(insn, 15, 5),
                (int)Pick(insn, 12, 3));

        private (uint imm, int rd) DecodeOperandU(uint insn) =>
            new ValueTuple<uint, int>(
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

        private uint Pick(uint insn, int lsb, int width = 1) => (insn >> lsb) & ((1u << width) - 1);

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
