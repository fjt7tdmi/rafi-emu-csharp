using System;

namespace Rafi.RV32I
{
    internal class LUI : IOp
    {
        private readonly int rd;
        private readonly uint imm;

        public LUI(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            core.IntReg[rd] = imm;
        }

        public override string ToString() =>
            $"lui {Names.IntReg[rd]},{imm}";
    }

    internal class AUIPC : IOp
    {
        private readonly int rd;
        private readonly uint imm;

        public AUIPC(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            core.IntReg[rd] = core.Pc + imm;
        }

        public override string ToString() =>
            $"auipc {Names.IntReg[rd]},{imm}";
    }

    internal class JAL : IOp
    {
        private readonly int rd;
        private readonly uint imm;

        public JAL(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var nextPc = core.NextPc;

            core.NextPc = core.Pc + imm;
            core.IntReg[rd] = nextPc;
        }

        public override string ToString() =>
            (rd == 0) ? $"j #{imm}" :
            $"jal {Names.IntReg[rd]},{imm}";
    }

    internal class JALR : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public JALR(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var nextPc = core.NextPc;

            core.NextPc = core.IntReg[rs1] + imm;
            core.IntReg[rd] = nextPc;
        }

        public override string ToString() =>
            (rd == 0) ? $"jr {Names.IntReg[rs1]},{imm}" :
            $"jalr {Names.IntReg[rd]},{Names.IntReg[rs1]},{imm}";
    }

    internal class BEQ : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BEQ(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            if (src1 == src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"beqz {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"beqz {Names.IntReg[rs1]}, #{imm}" :
            $"beq {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BNE : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BNE(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            if (src1 != src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bnez {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bnez {Names.IntReg[rs1]}, #{imm}" :
            $"bne {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BLT : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BLT(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            if (src1 < src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bltz {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bltz {Names.IntReg[rs1]}, #{imm}" :
            $"blt {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BGE : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BGE(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            if (src1 >= src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bgez {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bgez {Names.IntReg[rs1]}, #{imm}" :
            $"bge {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BLTU : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BLTU(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            if (src1 < src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            $"bltu {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BGEU : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public BGEU(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            if (src1 >= src2)
            {
                core.NextPc = core.Pc + imm;
            }
        }

        public override string ToString() =>
            $"bgeu {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class LB : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public LB(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = core.Bus.ReadUInt8(addr);

            core.IntReg[rd] = Utils.SignExtend(8, value);
        }

        public override string ToString() =>
            $"lb {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LH : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public LH(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = core.Bus.ReadUInt16(addr);

            core.IntReg[rd] = Utils.SignExtend(16, value);
        }

        public override string ToString() =>
            $"lh {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LW : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public LW(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = core.Bus.ReadUInt32(addr);

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"lw {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LBU : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public LBU(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = core.Bus.ReadUInt8(addr);

            core.IntReg[rd] = Utils.ZeroExtend(8, value);
        }

        public override string ToString() =>
            $"lbu {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LHU : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public LHU(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = core.Bus.ReadUInt16(addr);

            core.IntReg[rd] = Utils.ZeroExtend(16, value);
        }

        public override string ToString() =>
            $"lhu {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SB : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public SB(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = (byte)core.IntReg[rs2];

            core.Bus.WriteUInt8(addr, value);
        }

        public override string ToString() =>
            $"sb {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SH : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public SH(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = (ushort)core.IntReg[rs2];

            core.Bus.WriteUInt16(addr, value);
        }

        public override string ToString() =>
            $"sh {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SW : IOp
    {
        private readonly int rs1;
        private readonly int rs2;
        private readonly uint imm;

        public SW(int rs1, int rs2, uint imm)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var addr = core.IntReg[rs1] + imm;
            var value = (ushort)core.IntReg[rs2];

            core.Bus.WriteUInt32(addr, value);
        }

        public override string ToString() =>
            $"sw {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class ADDI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public ADDI(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 + imm;
            
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"addi {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLTI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public SLTI(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var value = (src1 < (int)imm) ? 1u : 0u;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"slti {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLTIU : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public SLTIU(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = (src1 < imm) ? 1u : 0u;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"sltiu {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class XORI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public XORI(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 ^ imm;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"xori {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class ORI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public ORI(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 | imm;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"ori {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class ANDI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly uint imm;

        public ANDI(int rd, int rs1, uint imm)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.imm = imm;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 & imm;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"andi {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLLI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int shamt;

        public SLLI(int rd, int rs1, int shamt)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.shamt = shamt;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 << shamt;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"slli {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class SRLI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int shamt;

        public SRLI(int rd, int rs1, int shamt)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.shamt = shamt;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var value = src1 >> shamt;

            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            $"srli {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class SRAI : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int shamt;

        public SRAI(int rd, int rs1, int shamt)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.shamt = shamt;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var value = src1 >> shamt;

            core.IntReg[rd] = (uint)value;
        }

        public override string ToString() =>
            $"srai {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class ADD : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public ADD(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = src1 + src2;
        }

        public override string ToString() =>
            $"add {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SUB : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SUB(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = src1 - src2;
        }

        public override string ToString() =>
            $"sub {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLL : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SLL(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            core.IntReg[rd] = src1 << src2;
        }

        public override string ToString() =>
            $"sll {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLT : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SLT(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            core.IntReg[rd] = (src1 < src2) ? 1u : 0u;
        }

        public override string ToString() =>
            $"slt {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLTU : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SLTU(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = (src1 < src2) ? 1u : 0u;
        }

        public override string ToString() =>
            $"sltu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class XOR : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public XOR(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = src1 ^ src2;
        }

        public override string ToString() =>
            $"xor {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SRL : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SRL(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            core.IntReg[rd] = src1 >> src2;
        }

        public override string ToString() =>
            $"srl {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SRA : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public SRA(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = (int)core.IntReg[rs1];
            var src2 = (int)core.IntReg[rs2];

            core.IntReg[rd] = (uint)(src1 >> src2);
        }

        public override string ToString() =>
            $"sra {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class OR : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public OR(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = src1 | src2;
        }

        public override string ToString() =>
            $"or {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class AND : IOp
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public AND(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public void Execute(Core core)
        {
            var src1 = core.IntReg[rs1];
            var src2 = core.IntReg[rs2];

            core.IntReg[rd] = src1 & src2;
        }

        public override string ToString() =>
            $"and {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class FENCE : IOp
    {
        private readonly int pred;
        private readonly int succ;

        public FENCE(int pred, int succ)
        {
            this.pred = pred;
            this.succ = succ;
        }

        public void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"fence";
    }

    internal class FENCE_I : IOp
    {
        public FENCE_I()
        {
        }

        public void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"fence.i";
    }

    internal class ECALL : IOp
    {
        public ECALL()
        {
        }

        public void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"ecall";
    }

    internal class EBREAK : IOp
    {
        public EBREAK()
        {
        }

        public void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"ebreak";
    }

    internal class CSRRW : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly int rs1;

        public CSRRW(int csr, int rd, int rs1)
        {
            this.csr = csr;
            this.rd = rd;
            this.rs1 = rs1;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = core.IntReg[rs1];
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrw {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrw {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRS : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly int rs1;

        public CSRRS(int csr, int rd, int rs1)
        {
            this.csr = csr;
            this.rd = rd;
            this.rs1 = rs1;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = value | core.IntReg[rs1];
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rs1 == 0) ? $"csrr {Names.IntReg[rd]},{Names.Csr[csr]}" :
            (rd == 0) ? $"csrr {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrs {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRC : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly int rs1;

        public CSRRC(int csr, int rd, int rs1)
        {
            this.csr = csr;
            this.rd = rd;
            this.rs1 = rs1;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = ~value & core.IntReg[rs1];
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrc {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrc {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRWI : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly uint zimm;

        public CSRRWI(int csr, int rd, uint zimm)
        {
            this.csr = csr;
            this.rd = rd;
            this.zimm = zimm;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = zimm;
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrwi {Names.Csr[csr]},{zimm}" :
            $"csrrwi {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }

    internal class CSRRSI : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly uint zimm;

        public CSRRSI(int csr, int rd, uint zimm)
        {
            this.csr = csr;
            this.rd = rd;
            this.zimm = zimm;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = value | zimm;
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrsi {Names.Csr[csr]},{zimm}" :
            $"csrrsi {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }

    internal class CSRRCI : IOp
    {
        private readonly int csr;
        private readonly int rd;
        private readonly uint zimm;

        public CSRRCI(int csr, int rd, uint zimm)
        {
            this.csr = csr;
            this.rd = rd;
            this.zimm = zimm;
        }

        public void Execute(Core core)
        {
            var value = core.Csr[csr];

            core.Csr[csr] = ~value & zimm;
            core.IntReg[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrci {Names.Csr[csr]},{zimm}" :
            $"csrrci {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }
}
