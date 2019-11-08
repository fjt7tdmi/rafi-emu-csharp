using System;

namespace Rafi
{
    internal class Names
    {
        public static string[] Csr { get; } = new string[0x1000];

        public static string[] IntReg { get; } = new string[32];

        static Names()
        {
            Csr[0x000] = "ustatus";
            Csr[0x001] = "fflags";
            Csr[0x002] = "frm";
            Csr[0x003] = "fcsr";
            Csr[0x004] = "uie";
            Csr[0x005] = "utvec";
            Csr[0x040] = "uscratch";
            Csr[0x041] = "uepc";
            Csr[0x042] = "ucause";
            Csr[0x043] = "utval";
            Csr[0x044] = "uip";

            Csr[0x100] = "sstatus";
            Csr[0x102] = "sedeleg";
            Csr[0x103] = "sideleg";
            Csr[0x104] = "sie";
            Csr[0x105] = "stvec";
            Csr[0x106] = "scounteren";
            Csr[0x140] = "sscratch";
            Csr[0x141] = "sepc";
            Csr[0x142] = "scause";
            Csr[0x143] = "stval";
            Csr[0x144] = "sip";
            Csr[0x180] = "satp";

            Csr[0x300] = "mstatus";
            Csr[0x301] = "misa";
            Csr[0x302] = "medeleg";
            Csr[0x303] = "mideleg";
            Csr[0x304] = "mie";
            Csr[0x305] = "mtvec";
            Csr[0x306] = "mcounteren";
            for (int i = 3; i < 32; i++)
            {
                Csr[0x320 + i] = $"mhpmevent{i}";
            }
            Csr[0x340] = "mscratch";
            Csr[0x341] = "mepc";
            Csr[0x342] = "mcause";
            Csr[0x343] = "mtval";
            Csr[0x344] = "mip";
            for (int i = 0; i < 4; i++)
            {
                Csr[0x3a0 + i] = $"pmpcfg{i}";
            }
            for (int i = 3; i < 16; i++)
            {
                Csr[0x3b0 + i] = $"pmpaddr{i}";
            }

            Csr[0x7a0] = "tselect";
            Csr[0x7a1] = "tdata1";
            Csr[0x7a2] = "tdata2";
            Csr[0x7a3] = "tdata3";
            Csr[0x7b0] = "dcsr";
            Csr[0x7b1] = "dpc";
            Csr[0x7b2] = "dscratch";

            Csr[0xb00] = "mcycle";
            Csr[0xb01] = "mtime";
            Csr[0xb02] = "minstret";
            for (int i = 3; i < 32; i++)
            {
                Csr[0xb00 + i] = $"mhpmcounter{i}";
            }
            Csr[0xb80] = "mcycleh";
            Csr[0xb81] = "mtimeh";
            Csr[0xb82] = "minstreth";
            for (int i = 3; i < 32; i++)
            {
                Csr[0xb80 + i] = $"mhpmcounter{i}h";
            }

            Csr[0xc00] = "cycle";
            Csr[0xc01] = "time";
            Csr[0xc02] = "instret";
            for (int i = 3; i < 32; i++)
            {
                Csr[0xc00 + i] = $"hpmcounter{i}";
            }
            Csr[0xc80] = "cycleh";
            Csr[0xc81] = "timeh";
            Csr[0xc82] = "instreth";
            for (int i = 3; i < 32; i++)
            {
                Csr[0xc80 + i] = $"hpmcounter{i}h";
            }

            Csr[0xf11] = "mvendorid";
            Csr[0xf12] = "marchid";
            Csr[0xf13] = "mimpid";
            Csr[0xf14] = "mhartid";

            IntReg[0] = "zero";
            IntReg[1] = "ra";
            IntReg[2] = "sp";
            IntReg[3] = "gp";
            IntReg[4] = "tp";
            IntReg[5] = "t0";
            IntReg[6] = "t1";
            IntReg[7] = "t2";
            IntReg[8] = "s0";
            IntReg[9] = "s1";
            IntReg[10] = "a0";
            IntReg[11] = "a1";
            IntReg[12] = "a2";
            IntReg[13] = "a3";
            IntReg[14] = "a4";
            IntReg[15] = "a5";
            IntReg[16] = "a6";
            IntReg[17] = "a7";
            IntReg[18] = "s2";
            IntReg[19] = "s3";
            IntReg[20] = "s4";
            IntReg[21] = "s5";
            IntReg[22] = "s6";
            IntReg[23] = "s7";
            IntReg[24] = "s8";
            IntReg[25] = "s9";
            IntReg[26] = "s10";
            IntReg[27] = "s11";
            IntReg[28] = "t3";
            IntReg[29] = "t4";
            IntReg[30] = "t5";
            IntReg[31] = "t6";
        }
    }
}
