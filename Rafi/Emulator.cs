using System;

namespace Rafi
{
    public class Emulator
    {
        [Flags]
        public enum StopCondition
        {
            HostIo = 1,
            Trap = 2,
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

        public void ProcessCycle()
        {
            processor.ProcessCycle();
        }

        public void Process(int cycle, StopCondition condition)
        {
            for (int i = 0; i < cycle; i++)
            {
                if (condition.HasFlag(StopCondition.HostIo))
                {
                    if (GetHostIoValue() != 0)
                    {
                        PrintHostIoValue(GetHostIoValue());
                        return;
                    }
                }

                ProcessCycle();
            }
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
