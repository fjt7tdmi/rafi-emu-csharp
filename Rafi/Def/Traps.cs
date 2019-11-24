using System;

namespace Rafi
{
    internal abstract class Trap
    {
        public TrapType TrapType { get; }
        public InterruptType? Interrupt { get; }
        public ExceptionType? Exception { get; }
        public ulong Cause { get; }
        public ulong Pc { get; }
        public ulong Value { get; }

        protected Trap(InterruptType interruptType, ulong pc, ulong value = 0)
        {
            TrapType = TrapType.Interrupt;
            Interrupt = interruptType;
            Cause = (ulong)interruptType;
            Pc = pc;
            Value = value;
        }

        protected Trap(ExceptionType exceptionType, ulong pc, ulong value = 0)
        {
            TrapType = TrapType.Exception;
            Exception = exceptionType;
            Cause = (ulong)exceptionType;
            Pc = pc;
            Value = value;
        }

        protected Trap(TrapType trapType, ulong pc)
        {
            TrapType = trapType;
            Pc = pc;
        }
    }

    internal class BreakpointException : Trap
    {
        public BreakpointException(ulong pc)
            : base(ExceptionType.Breakpoint, pc)
        {
        }
    }

    internal class EnvironmentCallFromMachineException : Trap
    {
        public EnvironmentCallFromMachineException(ulong pc)
            : base(ExceptionType.EnvironmentCallFromMachine, pc)
        {
        }
    }

    internal class TrapReturn : Trap
    {
        public TrapReturn(ulong pc)
            : base(TrapType.Return, pc)
        {
        }
    }
}
