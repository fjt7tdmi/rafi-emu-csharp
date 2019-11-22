using System;

namespace Rafi
{
    internal class Decoder64 : IDecoder
    {
        public Op Decode(uint insn) => DecodeRV64I(insn);
        
        private Op DecodeRV64I(uint insn)
        {
            var opcode = Utils.Pick(insn, 0, 7);

            var r = DecodeOperandR(insn);
            var r_shamt5 = DecodeOperandR_Shamt5(insn);
            var r_shamt6 = DecodeOperandR_Shamt6(insn);
            var i = DecodeOperandI(insn);
            var i_csr = DecodeOperandI_CSR(insn);
            var s = DecodeOperandS(insn);
            var b = DecodeOperandB(insn);
            var u = DecodeOperandU(insn);
            var j = DecodeOperandJ(insn);

            switch (opcode)
            {
                case 0b0110111:
                    return new RV64I.LUI(u.rd, u.imm);
                case 0b0010111:
                    return new RV64I.AUIPC(u.rd, u.imm);
                case 0b1101111:
                    return new RV64I.JAL(j.rd, j.imm);
                case 0b1100111:
                    return new RV64I.JALR(i.rd, i.rs1, i.imm);
                case 0b1100011:
                    switch(b.funct3)
                    {
                        case 0b000:
                            return new RV64I.BEQ(b.rs1, b.rs2, b.imm);
                        case 0b001:
                            return new RV64I.BNE(b.rs1, b.rs2, b.imm);
                        case 0b100:
                            return new RV64I.BLT(b.rs1, b.rs2, b.imm);
                        case 0b101:
                            return new RV64I.BGE(b.rs1, b.rs2, b.imm);
                        case 0b110:
                            return new RV64I.BLTU(b.rs1, b.rs2, b.imm);
                        case 0b111:
                            return new RV64I.BGEU(b.rs1, b.rs2, b.imm);
                        default:
                            return ThrowUnknownInsnException(insn);
                    }
                case 0b0000011:
                    switch (i.funct3)
                    {
                        case 0b000:
                            return new RV64I.LB(i.rd, i.rs1, i.imm);
                        case 0b001:
                            return new RV64I.LH(i.rd, i.rs1, i.imm);
                        case 0b010:
                            return new RV64I.LW(i.rd, i.rs1, i.imm);
                        case 0b011:
                            return new RV64I.LD(i.rd, i.rs1, i.imm);
                        case 0b100:
                            return new RV64I.LBU(i.rd, i.rs1, i.imm);
                        case 0b101:
                            return new RV64I.LHU(i.rd, i.rs1, i.imm);
                        case 0b110:
                            return new RV64I.LWU(i.rd, i.rs1, i.imm);
                        default:
                            return ThrowUnknownInsnException(insn);
                    }
                case 0b0100011:
                    switch (s.funct3)
                    {
                        case 0b000:
                            return new RV64I.SB(s.rs1, s.rs2, s.imm);
                        case 0b001:
                            return new RV64I.SH(s.rs1, s.rs2, s.imm);
                        case 0b010:
                            return new RV64I.SW(s.rs1, s.rs2, s.imm);
                        default:
                            return ThrowUnknownInsnException(insn);
                    }
                case 0b0010011:
                    switch (i.funct3)
                    {
                        case 0b000:
                            return new RV64I.ADDI(i.rd, i.rs1, i.imm);
                        case 0b001:
                            if (r_shamt6.funct6 == 0b000000)
                            {
                                return new RV64I.SLLI(r_shamt6.rd, r_shamt6.rs1, r_shamt6.shamt);
                            }
                            else
                            {
                                return ThrowUnknownInsnException(insn);
                            }
                        case 0b010:
                            return new RV64I.SLTI(i.rd, i.rs1, i.imm);
                        case 0b011:
                            return new RV64I.SLTIU(i.rd, i.rs1, i.imm);
                        case 0b100:
                            return new RV64I.XORI(i.rd, i.rs1, i.imm);
                        case 0b101:
                            if (r_shamt6.funct6 == 0b000000)
                            {
                                return new RV64I.SRLI(r_shamt6.rd, r_shamt6.rs1, r_shamt6.shamt);
                            }
                            else if (r_shamt6.funct6 == 0b010000)
                            {
                                return new RV64I.SRAI(r_shamt6.rd, r_shamt6.rs1, r_shamt6.shamt);
                            }
                            else
                            {
                                return ThrowUnknownInsnException(insn);
                            }
                        case 0b110:
                            return new RV64I.ORI(i.rd, i.rs1, i.imm);
                        case 0b111:
                            return new RV64I.ANDI(i.rd, i.rs1, i.imm);
                        default:
                            return ThrowUnknownInsnException(insn);
                    }
                case 0b0110011:
                    if (r.funct3 == 0b000 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.ADD(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b000 && r.funct7 == 0b0100000)
                    {
                        return new RV64I.SUB(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b001 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SLL(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b010 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SLT(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b011 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SLTU(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b100 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.XOR(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SRL(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0100000)
                    {
                        return new RV64I.SRA(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b110 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.OR(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b111 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.AND(r.rd, r.rs1, r.rs2);
                    }
                    else
                    {
                        return ThrowUnknownInsnException(insn);
                    }
                case 0b0001111:
                    if (i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000 && Utils.Pick(insn, 28, 4) == 0b0000)
                    {
                        return new RV64I.FENCE((int)Utils.Pick(insn, 24, 4), (int)Utils.Pick(insn, 20, 4));
                    }
                    else if (i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000 && i.imm == 0b0000_0000_0000)
                    {
                        return new RV64I.FENCE_I();
                    }
                    else
                    {
                        return ThrowUnknownInsnException(insn);
                    }
                case 0b1110011:
                    if (i.imm == 0b0000_0000_0010 && r.rs1 == 0b00000 && r.funct3 == 0b000 && r.rd == 0b00000)
                    {
                        return new RV64I.URET();
                    }
                    else if (i.imm == 0b0001_0000_0010 && r.rs1 == 0b00000 && r.funct3 == 0b000 && r.rd == 0b00000)
                    {
                        return new RV64I.SRET();
                    }
                    else if (i.imm == 0b0011_0000_0010 && r.rs1 == 0b00000 && r.funct3 == 0b000 && r.rd == 0b00000)
                    {
                        return new RV64I.MRET();
                    }
                    else if (i.imm == 0b0001_0000_0101 && r.rs1 == 0b00000 && r.funct3 == 0b000 && r.rd == 0b00000)
                    {
                        return new RV64I.WFI();
                    }
                    else if (i.imm == 0b0001_0000_0101 && r.rs1 == 0b00000 && r.funct3 == 0b000 && r.rd == 0b00000)
                    {
                        return new RV64I.SFENCE_VMA(r.rs1, r.rs2);
                    }
                    else if (i.imm == 0b0000_0000_0000 && i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000)
                    {
                        return new RV64I.ECALL();
                    }
                    else if (i.imm == 0b0000_0000_0001 && i.rs1 == 0b00000 && i.funct3 == 0b000 && i.rd == 0b00000)
                    {
                        return new RV64I.EBREAK();
                    }
                    else
                    {
                        switch (i_csr.funct3)
                        {
                            case 0b001:
                                return new RV64I.CSRRW(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b010:
                                return new RV64I.CSRRS(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b011:
                                return new RV64I.CSRRC(i_csr.csr, i_csr.rd, i_csr.rs1);
                            case 0b101:
                                return new RV64I.CSRRWI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            case 0b110:
                                return new RV64I.CSRRSI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            case 0b111:
                                return new RV64I.CSRRCI(i_csr.csr, i_csr.rd, i_csr.zimm);
                            default:
                                return ThrowUnknownInsnException(insn);
                        }
                    }
                case 0b0011011:
                    if (i.funct3 == 0b000)
                    {
                        return new RV64I.ADDIW(i.rd, i.rs1, i.imm);
                    }
                    else if (r_shamt5.funct3 == 0b001 && r_shamt5.funct7 == 0b0000000)
                    {
                        return new RV64I.SLLIW(r_shamt5.rd, r_shamt5.rs1, r_shamt5.shamt);
                    }
                    else if (r_shamt5.funct3 == 0b101 && r_shamt5.funct7 == 0b0000000)
                    {
                        return new RV64I.SRLIW(r_shamt5.rd, r_shamt5.rs1, r_shamt5.shamt);
                    }
                    else if (r_shamt5.funct3 == 0b0011011 && r_shamt5.funct7 == 0b0100000)
                    {
                        return new RV64I.SRAIW(r_shamt5.rd, r_shamt5.rs1, r_shamt5.shamt);
                    }
                    else
                    {
                        return ThrowUnknownInsnException(insn);
                    }
                case 0b0111011:
                    if (r.funct3 == 0b000 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.ADDW(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b000 && r.funct7 == 0b0100000)
                    {
                        return new RV64I.SUBW(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b001 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SLLW(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0000000)
                    {
                        return new RV64I.SRLW(r.rd, r.rs1, r.rs2);
                    }
                    else if (r.funct3 == 0b101 && r.funct7 == 0b0100000)
                    {
                        return new RV64I.SRAW(r.rd, r.rs1, r.rs2);
                    }
                    else
                    {
                        return ThrowUnknownInsnException(insn);
                    }
                default:
                    return ThrowUnknownInsnException(insn);
            }
        }

        private Op ThrowUnknownInsnException(uint insn)
        {
            throw new FormatException($"Unknown insn 0x{insn:x}");
        }

        private (int funct7, int rs2, int rs1, int funct3, int rd) DecodeOperandR(uint insn) =>
            new ValueTuple<int, int, int, int, int>(
                (int)Utils.Pick(insn, 25, 7),
                (int)Utils.Pick(insn, 20, 5),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3),
                (int)Utils.Pick(insn, 7, 5));

        private (int funct7, int shamt, int rs1, int funct3, int rd) DecodeOperandR_Shamt5(uint insn) =>
            new ValueTuple<int, int, int, int, int>(
                (int)Utils.Pick(insn, 25, 7),
                (int)Utils.Pick(insn, 20, 5),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3),
                (int)Utils.Pick(insn, 7, 5));

        private (int funct6, int shamt, int rs1, int funct3, int rd) DecodeOperandR_Shamt6(uint insn) =>
            new ValueTuple<int, int, int, int, int>(
                (int)Utils.Pick(insn, 26, 6),
                (int)Utils.Pick(insn, 20, 6),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3),
                (int)Utils.Pick(insn, 7, 5));

        private (uint imm, int rs1, int funct3, int rd) DecodeOperandI(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Utils.SignExtend32(12,
                    Utils.Pick(insn, 20, 12)),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3),
                (int)Utils.Pick(insn, 7, 5));

        private (int csr, uint zimm, int rs1, int funct3, int rd) DecodeOperandI_CSR(uint insn) =>
            new ValueTuple<int, uint, int, int, int>(
                (int)Utils.Pick(insn, 20, 12),
                Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3),
                (int)Utils.Pick(insn, 7, 5));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandS(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Utils.SignExtend32(12,
                    Utils.Pick(insn, 25, 7) << 5 |
                    Utils.Pick(insn, 7, 5)),
                (int)Utils.Pick(insn, 20, 5),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3));

        private (uint imm, int rs2, int rs1, int funct3) DecodeOperandB(uint insn) =>
            new ValueTuple<uint, int, int, int>(
                Utils.SignExtend32(13,
                    Utils.Pick(insn, 31) << 12 |
                    Utils.Pick(insn, 7) << 11 |
                    Utils.Pick(insn, 25, 6) << 5 |
                    Utils.Pick(insn, 8, 4) << 1),
                (int)Utils.Pick(insn, 20, 5),
                (int)Utils.Pick(insn, 15, 5),
                (int)Utils.Pick(insn, 12, 3));

        private (uint imm, int rd) DecodeOperandU(uint insn) =>
            new ValueTuple<uint, int>(
                Utils.Pick(insn, 12, 20) << 12,
                (int)Utils.Pick(insn, 7, 5));

        private (uint imm, int rd) DecodeOperandJ(uint insn) =>
            new ValueTuple<uint, int>(
                Utils.SignExtend32(21,
                    Utils.Pick(insn, 31) << 20 |
                    Utils.Pick(insn, 21, 10) << 1 |
                    Utils.Pick(insn, 20) << 11 |
                    Utils.Pick(insn, 12, 8) << 12
                ),
                (int)Utils.Pick(insn, 7, 5));
    }
}
