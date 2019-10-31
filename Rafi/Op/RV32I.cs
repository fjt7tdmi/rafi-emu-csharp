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
}
