using System;

namespace Rafi.RV32M
{
    internal class MUL : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public MUL(int rd, int rs1, int rs2)
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

            x[rd] = src1 * src2;
        }

        public override string ToString() =>
            $"mul {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class MULH : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public MULH(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = Utils.SignExtend64(32, x[rs1]);
            var src2 = Utils.SignExtend64(32, x[rs2]);

            x[rd] = (uint)((src1 * src2) >> 32);
        }

        public override string ToString() =>
            $"mulh {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class MULHSU : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public MULHSU(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = Utils.SignExtend64(32, x[rs1]);
            var src2 = Utils.ZeroExtend64(32, x[rs2]);

            x[rd] = (uint)((src1 * src2) >> 32);
        }

        public override string ToString() =>
            $"mulhsu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class MULHU : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public MULHU(int rd, int rs1, int rs2)
        {
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
        }

        public override void Execute(Core core)
        {
            var x = core.IntReg32;
            var src1 = Utils.ZeroExtend64(32, x[rs1]);
            var src2 = Utils.ZeroExtend64(32, x[rs2]);

            x[rd] = (uint)((src1 * src2) >> 32);
        }

        public override string ToString() =>
            $"mulhu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class DIV : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public DIV(int rd, int rs1, int rs2)
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

            if (src1 == 0x80000000 && src2 == 0xffffffff)
            {
                x[rd] = 0x80000000;
            }
            else if (src2 == 0)
            {
                x[rd] = 0xffffffff;
            }
            else
            {
                x[rd] = (uint)((int)src1 / (int)src2);
            }
        }

        public override string ToString() =>
            $"div {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class DIVU : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public DIVU(int rd, int rs1, int rs2)
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

            if (src2 == 0)
            {
                x[rd] = 0xffffffff;
            }
            else
            {
                x[rd] = src1 / src2;
            }
        }

        public override string ToString() =>
            $"divu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class REM : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public REM(int rd, int rs1, int rs2)
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

            if (src1 == 0x80000000 && src2 == 0xffffffff)
            {
                x[rd] = 0;
            }
            else if (src2 == 0)
            {
                x[rd] = src1;
            }
            else
            {
                x[rd] = (uint)((int)src1 % (int)src2);
            }
        }

        public override string ToString() =>
            $"rem {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }

    internal class REMU : Op
    {
        private readonly int rd;
        private readonly int rs1;
        private readonly int rs2;

        public REMU(int rd, int rs1, int rs2)
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

            if (src2 == 0)
            {
                x[rd] = src1;
            }
            else
            {
                x[rd] = src1 % src2;
            }
        }

        public override string ToString() =>
            $"remu {Names.IntReg[rd]},{Names.IntReg[rs1]},{Names.IntReg[rs2]}";
    }
}
