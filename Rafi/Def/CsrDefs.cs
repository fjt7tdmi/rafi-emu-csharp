using System;

namespace Rafi
{
    internal class MSTATUS : CsrDef
    {
        public MSTATUS()
            : base(0)
        {
        }

        public MSTATUS(uint value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MSTATUS;

        // Status Dirty
        public uint SD
        {
            get => Get(31);
            set => Set(31, value);
        }

        // Trap SRET
        public uint TSR
        {
            get => Get(22);
            set => Set(22, value);
        }

        // Timeout Wait
        public uint TW
        {
            get => Get(21);
            set => Set(21, value);
        }

        // Trap Virtual Memory
        public uint TVM
        {
            get => Get(20);
            set => Set(20, value);
        }

        // Make eXecutable Readable
        public uint MXR
        {
            get => Get(19);
            set => Set(19, value);
        }

        // permit Supervisor User Memory access
        public uint SUM
        {
            get => Get(18);
            set => Set(18, value);
        }

        // Modify PRiVilege
        public uint MPRV
        {
            get => Get(17);
            set => Set(17, value);
        }

        // additional user-mode eXtensions Status
        public uint XS
        {
            get => Get(16, 15);
            set => Set(16, 15, value);
        }

        // Floating-point Status
        public uint FS
        {
            get => Get(14, 13);
            set => Set(14, 13, value);
        }

        // Machine Previous Priviledged mode
        public uint MPP
        {
            get => Get(12, 11);
            set => Set(12, 11, value);
        }

        // Supervisor Previous Priviledged mode
        public uint SPP
        {
            get => Get(8);
            set => Set(8, value);
        }

        // Machine Previous Interrupt Enable
        public uint MPIE
        {
            get => Get(7);
            set => Set(7, value);
        }

        // Supervisor Previous Interrupt Enable
        public uint SPIE
        {
            get => Get(5);
            set => Set(5, value);
        }

        // User Previous Interrupt Enable
        public uint UPIE
        {
            get => Get(4);
            set => Set(4, value);
        }

        // Machine Interrupt Enable
        public uint MIE
        {
            get => Get(3);
            set => Set(3, value);
        }

        // Supervisor Interrupt Enable
        public uint SIE
        {
            get => Get(1);
            set => Set(1, value);
        }

        // User Interrupt Enable
        public uint UIE
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

        public MTVEC(uint value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MTVEC;

        public uint BASE
        {
            get => Get(31, 2);
            set => Set(31, 2, value);
        }

        public uint MODE
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

        public MEPC(uint value)
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

        public MCAUSE(uint value)
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

        public MTVAL(uint value)
            : base(value)
        {
        }

        public override CsrAddr CsrAddr { get; } = CsrAddr.MTVAL;
    }
}
