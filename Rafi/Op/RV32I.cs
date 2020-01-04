using System;

namespace Rafi.RV32I
{
    internal class LUI : Op
    {
        private readonly int rd;
        private readonly uint imm;

        public LUI(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            x[rd] = imm;
        }

        public override string ToString() =>
            $"lui {Names.IntReg[rd]},{imm}";
    }

    internal class AUIPC : Op
    {
        private readonly int rd;
        private readonly uint imm;

        public AUIPC(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            x[rd] = core.Pc32 + imm;
        }

        public override string ToString() =>
            $"auipc {Names.IntReg[rd]},{imm}";
    }

    internal class JAL : Op
    {
        private readonly int rd;
        private readonly uint imm;

        public JAL(int rd, uint imm)
        {
            this.rd = rd;
            this.imm = imm;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var nextPc = core.NextPc32;

            core.NextPc32 = core.Pc32 + imm;
            x[rd] = nextPc;
        }

        public override string ToString() =>
            (rd == 0) ? $"j #{imm}" :
            $"jal {Names.IntReg[rd]},{imm}";
    }

    internal class JALR : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var nextPc = core.NextPc32;

            core.NextPc32 = x[rs1] + imm;
            x[rd] = nextPc;
        }

        public override string ToString() =>
            (rd == 0) ? $"jr {Names.IntReg[rs1]},{imm}" :
            $"jalr {Names.IntReg[rd]},{Names.IntReg[rs1]},{imm}";
    }

    internal class BEQ : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if (x[rs1] == x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"beqz {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"beqz {Names.IntReg[rs1]}, #{imm}" :
            $"beq {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BNE : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if (x[rs1] != x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bnez {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bnez {Names.IntReg[rs1]}, #{imm}" :
            $"bne {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BLT : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if ((int)x[rs1] < (int)x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bltz {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bltz {Names.IntReg[rs1]}, #{imm}" :
            $"blt {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BGE : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if ((int)x[rs1] >= (int)x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            (rs1 == 0) ? $"bgez {Names.IntReg[rs2]}, #{imm}" :
            (rs2 == 0) ? $"bgez {Names.IntReg[rs1]}, #{imm}" :
            $"bge {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BLTU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if (x[rs1] < x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            $"bltu {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class BGEU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;

            if (x[rs1] >= x[rs2])
            {
                core.NextPc32 = core.Pc32 + imm;
            }
        }

        public override string ToString() =>
            $"bgeu {Names.IntReg[rs1]},{Names.IntReg[rs2]},{imm}";
    }

    internal class LB : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = core.Bus.ReadUInt8(addr);

            x[rd] = Utils.SignExtend32(8, value);
        }

        public override string ToString() =>
            $"lb {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LH : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = core.Bus.ReadUInt16(addr);

            x[rd] = Utils.SignExtend32(16, value);
        }

        public override string ToString() =>
            $"lh {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LW : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = core.Bus.ReadUInt32(addr);

            x[rd] = value;
        }

        public override string ToString() =>
            $"lw {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LBU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = core.Bus.ReadUInt8(addr);

            x[rd] = Utils.ZeroExtend32(8, value);
        }

        public override string ToString() =>
            $"lbu {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class LHU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = core.Bus.ReadUInt16(addr);

            x[rd] = Utils.ZeroExtend32(16, value);
        }

        public override string ToString() =>
            $"lhu {Names.IntReg[rd]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SB : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = (byte)x[rs2];

            core.Bus.WriteUInt8(addr, value);
        }

        public override string ToString() =>
            $"sb {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SH : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = (ushort)x[rs2];

            core.Bus.WriteUInt16(addr, value);
        }

        public override string ToString() =>
            $"sh {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class SW : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var addr = x[rs1] + imm;
            var value = x[rs2];

            core.Bus.WriteUInt32(addr, value);
        }

        public override string ToString() =>
            $"sw {Names.IntReg[rs2]},{imm}({Names.IntReg[rs1]})";
    }

    internal class ADDI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 + imm;
            
            x[rd] = value;
        }

        public override string ToString() =>
            $"addi {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLTI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = (int)x[rs1];
            var value = (src1 < (int)imm) ? 1u : 0u;

            x[rd] = value;
        }

        public override string ToString() =>
            $"slti {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLTIU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = (src1 < imm) ? 1u : 0u;

            x[rd] = value;
        }

        public override string ToString() =>
            $"sltiu {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class XORI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 ^ imm;

            x[rd] = value;
        }

        public override string ToString() =>
            $"xori {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class ORI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 | imm;

            x[rd] = value;
        }

        public override string ToString() =>
            $"ori {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class ANDI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 & imm;

            x[rd] = value;
        }

        public override string ToString() =>
            $"andi {Names.IntReg[rd]},{Names.IntReg[rs1]},{(int)imm}";
    }

    internal class SLLI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 << shamt;

            x[rd] = value;
        }

        public override string ToString() =>
            $"slli {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class SRLI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var value = src1 >> shamt;

            x[rd] = value;
        }

        public override string ToString() =>
            $"srli {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class SRAI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = (int)x[rs1];
            var value = src1 >> shamt;

            x[rd] = (uint)value;
        }

        public override string ToString() =>
            $"srai {Names.IntReg[rd]},{Names.IntReg[rs1]},0x{shamt:x}";
    }

    internal class ADD : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = src1 + src2;
        }

        public override string ToString() =>
            $"add {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SUB : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = src1 - src2;
        }

        public override string ToString() =>
            $"sub {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLL : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = (int)x[rs2];

            x[rd] = src1 << src2;
        }

        public override string ToString() =>
            $"sll {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLT : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = (int)x[rs1];
            var src2 = (int)x[rs2];

            x[rd] = (src1 < src2) ? 1u : 0u;
        }

        public override string ToString() =>
            $"slt {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SLTU : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = (src1 < src2) ? 1u : 0u;
        }

        public override string ToString() =>
            $"sltu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class XOR : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = src1 ^ src2;
        }

        public override string ToString() =>
            $"xor {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SRL : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = (int)x[rs2];

            x[rd] = src1 >> src2;
        }

        public override string ToString() =>
            $"srl {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class SRA : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = (int)x[rs1];
            var src2 = (int)x[rs2];

            x[rd] = (uint)(src1 >> src2);
        }

        public override string ToString() =>
            $"sra {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class OR : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = src1 | src2;
        }

        public override string ToString() =>
            $"or {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class AND : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = x[rs1];
            var src2 = x[rs2];

            x[rd] = src1 & src2;
        }

        public override string ToString() =>
            $"and {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class FENCE : Op
    {
        private readonly int pred;
        private readonly int succ;

        public FENCE(int pred, int succ)
        {
            this.pred = pred;
            this.succ = succ;
        }

        public override void Execute(Core core)
        {
        }

        public override string ToString() =>
            $"fence";
    }

    internal class FENCE_I : Op
    {
        public FENCE_I()
        {
        }

        public override void Execute(Core core)
        {
        }

        public override string ToString() =>
            $"fence.i";
    }

    internal class ECALL : Op
    {
        public ECALL()
        {
        }

        public override Trap PostCheckTrap(Core core)
        {
            return new EnvironmentCallFromMachineException(core.Pc32);
        }

        public override string ToString() =>
            $"ecall";
    }

    internal class EBREAK : Op
    {
        public EBREAK()
        {
        }

        public override Trap PostCheckTrap(Core core)
        {
            return new BreakpointException(core.Pc32);
        }

        public override string ToString() =>
            $"ebreak";
    }

    internal class CSRRW : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = x[rs1];
            x[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrw {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrw {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRS : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = value | x[rs1];
            x[rd] = value;
        }

        public override string ToString() =>
            (rs1 == 0) ? $"csrr {Names.IntReg[rd]},{Names.Csr[csr]}" :
            (rd == 0) ? $"csrr {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrs {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRC : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = ~value & x[rs1];
            x[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrc {Names.Csr[csr]},{Names.IntReg[rs1]}" :
            $"csrrc {Names.IntReg[rd]},{Names.Csr[csr]},{Names.IntReg[rs1]}";
    }

    internal class CSRRWI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = zimm;
            x[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrwi {Names.Csr[csr]},{zimm}" :
            $"csrrwi {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }

    internal class CSRRSI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = value | zimm;
            x[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrsi {Names.Csr[csr]},{zimm}" :
            $"csrrsi {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }

    internal class CSRRCI : Op
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

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var csr = core.Csr32;

            var value = csr[this.csr];
            csr[this.csr] = ~value & zimm;
            x[rd] = value;
        }

        public override string ToString() =>
            (rd == 0) ? $"csrci {Names.Csr[csr]},{zimm}" :
            $"csrrci {Names.IntReg[rd]},{Names.Csr[csr]},{zimm}";
    }

    internal class URET : Op
    {
        public URET()
        {
        }

        public override Trap PostCheckTrap(Core core)
        {
            return new TrapReturn(core.Pc32);
        }

        public override string ToString() =>
            $"uret";
    }

    internal class SRET : Op
    {
        public SRET()
        {
        }

        public override Trap PostCheckTrap(Core core)
        {
            return new TrapReturn(core.Pc32);
        }

        public override string ToString() =>
            $"sret";
    }

    internal class MRET : Op
    {
        public MRET()
        {
        }

        public override Trap PostCheckTrap(Core core)
        {
            return new TrapReturn(core.Pc32);
        }

        public override string ToString() =>
            $"mret";
    }

    internal class WFI : Op
    {
        public WFI()
        {
        }

        public override void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"wfi";
    }

    internal class SFENCE_VMA : Op
    {
        private readonly int rs1;
        private readonly int rs2;

        public SFENCE_VMA(int rs1, int rs2)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public override void Execute(Core core)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"sfence.vma {Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }
}
