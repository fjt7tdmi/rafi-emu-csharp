using System;

namespace Rafi
{
    internal class MSTATUS : CsrDef
    {
        public MSTATUS()
            : base(0)
        {
        }

        public MSTATUS(ulong value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MSTATUS;

        // Status Dirty
        public ulong SD
        {
            get => Get(31);
            set => Set(31, value);
        }

        // Trap SRET
        public ulong TSR
        {
            get => Get(22);
            set => Set(22, value);
        }

        // Timeout Wait
        public ulong TW
        {
            get => Get(21);
            set => Set(21, value);
        }

        // Trap Virtual Memory
        public ulong TVM
        {
            get => Get(20);
            set => Set(20, value);
        }

        // Make eXecutable Readable
        public ulong MXR
        {
            get => Get(19);
            set => Set(19, value);
        }

        // permit Supervisor User Memory access
        public ulong SUM
        {
            get => Get(18);
            set => Set(18, value);
        }

        // Modify PRiVilege
        public ulong MPRV
        {
            get => Get(17);
            set => Set(17, value);
        }

        // additional user-mode eXtensions Status
        public ulong XS
        {
            get => Get(16, 15);
            set => Set(16, 15, value);
        }

        // Floating-point Status
        public ulong FS
        {
            get => Get(14, 13);
            set => Set(14, 13, value);
        }

        // Machine Previous Priviledged mode
        public ulong MPP
        {
            get => Get(12, 11);
            set => Set(12, 11, value);
        }

        // Supervisor Previous Priviledged mode
        public ulong SPP
        {
            get => Get(8);
            set => Set(8, value);
        }

        // Machine Previous Interrupt Enable
        public ulong MPIE
        {
            get => Get(7);
            set => Set(7, value);
        }

        // Supervisor Previous Interrupt Enable
        public ulong SPIE
        {
            get => Get(5);
            set => Set(5, value);
        }

        // User Previous Interrupt Enable
        public ulong UPIE
        {
            get => Get(4);
            set => Set(4, value);
        }

        // Machine Interrupt Enable
        public ulong MIE
        {
            get => Get(3);
            set => Set(3, value);
        }

        // Supervisor Interrupt Enable
        public ulong SIE
        {
            get => Get(1);
            set => Set(1, value);
        }

        // User Interrupt Enable
        public ulong UIE
        {
            get => Get(0);
            set => Set(0, value);
        }
    }

    internal class MTVEC : CsrDef
    {
        public MTVEC()
            : base(0)
        {
        }

        public MTVEC(ulong value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MTVEC;

        public ulong BASE
        {
            get => Get(31, 2);
            set => Set(31, 2, value);
        }

        public ulong MODE
        {
            get => Get(1, 0);
            set => Set(1, 0, value);
        }
    }

    internal class MEPC : CsrDef
    {
        public MEPC()
            : base(0)
        {
        }

        public MEPC(ulong value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MEPC;
    }

    internal class MCAUSE : CsrDef
    {
        public MCAUSE()
            : base(0)
        {
        }

        public MCAUSE(ulong value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MCAUSE;
    }

    internal class MTVAL : CsrDef
    {
        public MTVAL()
            : base(0)
        {
        }

        public MTVAL(ulong value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MTVAL;
    }
}
