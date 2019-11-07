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
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
    }
}
