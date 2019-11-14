﻿using System;

namespace Rafi
{
    internal enum CsrAddr : int
    {
        USTATUS = 0x000,
        FFLAGS = 0x001,
        FRM = 0x002,
        FCSR = 0x003,
        UIE = 0x004,
        UTVEC = 0x005,
        USCRATCH = 0x040,
        UEPC = 0x041,
        UCAUSE = 0x042,
        UTVAL = 0x043,
        UIP = 0x044,

        SSTATUS = 0x100,
        SEDELEG = 0x102,
        SIDELEG = 0x103,
        SIE = 0x104,
        STVEC = 0x105,
        SCOUNTEREN = 0x106,
        SSCRATCH = 0x140,
        SEPC = 0x141,
        SCAUSE = 0x142,
        STVAL = 0x143,
        SIP = 0x144,
        SATP = 0x180,

        MSTATUS = 0x300,
        MISA = 0x301,
        MEDELEG = 0x302,
        MIDELEG = 0x303,
        MIE = 0x304,
        MTVEC = 0x305,
        MCOUNTEREN = 0x306,
        MSCRATCH = 0x340,
        MEPC = 0x341,
        MCAUSE = 0x342,
        MTVAL = 0x343,
        MIP = 0x344,
        PMPCFG0 = 0x3a0,
        PMPCFG1 = 0x3a1,
        PMPCFG2 = 0x3a2,
        PMPCFG3 = 0x3a3,
        PMPADDR0 = 0x3b0,
        PMPADDR1 = 0x3b1,
        PMPADDR2 = 0x3b2,
        PMPADDR3 = 0x3b3,
        PMPADDR4 = 0x3b4,
        PMPADDR5 = 0x3b5,
        PMPADDR6 = 0x3b6,
        PMPADDR7 = 0x3b7,
        PMPADDR8 = 0x3b8,
        PMPADDR9 = 0x3b9,
        PMPADDR10 = 0x3ba,
        PMPADDR11 = 0x3bb,
        PMPADDR12 = 0x3bc,
        PMPADDR13 = 0x3bd,
        PMPADDR14 = 0x3be,
        PMPADDR15 = 0x3bf,

        TSELECT = 0x7a0,
        TDATA1 = 0x7a1,
        TDATA2 = 0x7a2,
        TDATA3 = 0x7a3,
        DCSR = 0x7b0,
        DPC = 0x7b1,
        DSCRATCH = 0x7b2,

        MCYCLE = 0xb00,
        MTIME = 0xb01,
        MINSTRET = 0xb02,
        MCYCLEH = 0xb80,
        MTIMEH = 0xb81,
        MINSTRETH = 0xb82,

        CYCLE = 0xc00,
        TIME = 0xc01,
        INSTRET = 0xc02,
        CYCLEH = 0xc80,
        TIMEH = 0xc81,
        INSTRETH = 0xc82,
        MVENDORID = 0xf11,
        MARCHID = 0xf12,
        MIMPID = 0xf13,
        MHARTID = 0xf14,
    }
}