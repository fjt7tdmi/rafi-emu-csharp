using System;

namespace Rafi
{
    public class Emulator
    {
        [Flags]
        public enum StopCondition
        {
            HostIo = 1,
            Breakpoint = 2,
        }

        private readonly Memory memory;
        private readonly Processor processor;

        internal Bus Bus { get; }

        internal Core Core
        {
            get => processor.Core;
        }

        public uint HostIoAddr { get; set; } = 0x80001000;

        public uint Pc
        {
            set => processor.Core.Pc32 = value;
        }

        public Emulator(int xlen)
        {
            memory = new Memory();
            Bus = new Bus(memory);
            processor = new Processor(xlen, Bus);
        }

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }

        internal Cycle ProcessCycle() => processor.ProcessCycle();

        public void Process(StopCondition condition)
        {
            while (true)
            {
                if (PreCheckStopCondition(condition))
                {
                    break;
                }

                var cycle = ProcessCycle();

                if (PostCheckStopCondition(cycle, condition))
                {
                    break;
                }
            }
        }

        public void Process(int cycle, StopCondition condition)
        {
            for (int i = 0; i < cycle; i++)
            {
                if (PreCheckStopCondition(condition))
                {
                    break;
                }

                var cycleLog = ProcessCycle();

                if (PostCheckStopCondition(cycleLog, condition))
                {
                    break;
                }
            }
        }

        private bool PreCheckStopCondition(StopCondition condition)
        {
            if (condition.HasFlag(StopCondition.HostIo))
            {
                if (GetHostIoValue() != 0)
                {
                    PrintHostIoValue(GetHostIoValue());
                    return true;
                }
            }

            return false;
        }

        private bool PostCheckStopCondition(Cycle cycle, StopCondition condition)
        {
            if (condition.HasFlag(StopCondition.Breakpoint))
            {
                if (cycle.Trap != null)
                {
                    return cycle.Trap.Exception == ExceptionType.Breakpoint;
                }
            }

            return false;
        }

        private uint GetHostIoValue() => Bus.ReadUInt32(HostIoAddr);

        private void PrintHostIoValue(uint value)
        {
            if (value == 1)
            {
                Console.WriteLine($"HostIo: {value} (success)");
            }
            else
            {
                Console.WriteLine($"HostIo: {value} (failure: testId={value / 2})");
            }
        }
    }
}
