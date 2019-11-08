using System;

namespace Rafi
{
    internal abstract class Trap
    {
        public TrapType TrapType { get; }
        public InterruptType? Interrupt { get; }
        public ExceptionType? Exception { get; }
        public uint Pc { get; }
        public uint Value { get; }

        protected Trap(InterruptType interruptType, uint pc, uint value = 0)
        {
            TrapType = TrapType.Interrupt;
            Interrupt = interruptType;
            Pc = pc;
            Value = value;
        }

        protected Trap(ExceptionType exceptionType, uint pc, uint value = 0)
        {
            TrapType = TrapType.Interrupt;
            Exception = exceptionType;
            Pc = pc;
            Value = value;
        }
    }

    internal class BreakpointException : Trap
    {
        public BreakpointException(uint pc)
            : base(ExceptionType.Breakpoint, pc)
        {
        }
    }

    internal class EnvironmentCallFromMachineException : Trap
    {
        public EnvironmentCallFromMachineException(uint pc)
            : base(ExceptionType.EnvironmentCallFromMachine, pc)
        {
        }
    }
}
