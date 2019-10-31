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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
