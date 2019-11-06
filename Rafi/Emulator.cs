using System;

namespace Rafi
{
    public class Emulator
    {
        private readonly Memory memory;
        private readonly Bus bus;
        private readonly Processor processor;

        internal Core Core
        {
            get => processor.Core;
        }

        public Emulator()
        {
            memory = new Memory();
            bus = new Bus(memory);
            processor = new Processor(bus);
        }

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }

        public void Process(int cycle)
        {
            for (int i = 0; i < cycle; i++)
            {
                processor.ProcessCycle();
            }
        }
    }
}
