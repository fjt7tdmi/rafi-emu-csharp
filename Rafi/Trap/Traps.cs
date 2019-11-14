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
            TrapType = TrapType.Exception;
            Exception = exceptionType;
            Pc = pc;
            Value = value;
        }

        protected Trap(TrapType trapType, uint pc)
        {
            TrapType = trapType;
            Pc = pc;
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

    internal class TrapReturn : Trap
    {
        public TrapReturn(uint pc)
            : base(TrapType.Return, pc)
        {
        }
    }
}
