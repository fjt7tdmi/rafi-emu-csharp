using System;

namespace Rafi
{
    internal enum TrapType : uint
    {
        Interrupt = 0,
        Exception = 1,
        Return = 2,
    }

    internal enum InterruptType : uint
    {
        UserSoftware = 0,
        SupervisorSoftware = 1,
        MachineSoftware = 3,
        UserTimer = 4,
        SupervisorTimer = 5,
        MachineTimer = 7,
        UserExternal = 8,
        SupervisorExternal = 9,
        MachineExternal = 11,
    }

    internal enum ExceptionType : uint
    {
        InstructionAddressMisaligned = 0,
        InstructionAccessFault = 1,
        IllegalInstruction = 2,
        Breakpoint = 3,
        LoadAddressMisaligned = 4,
        LoadAccessFault = 5,
        StoreAddressMisaligned = 6,
        StoreAccessFault = 7,
        EnvironmentCallFromUser = 8,
        EnvironmentCallFromSupervisor = 9,
        EnvironmentCallFromMachine = 11,
        InstructionPageFault = 12,
        LoadPageFault = 13,
        StorePageFault = 15,
    }
}
