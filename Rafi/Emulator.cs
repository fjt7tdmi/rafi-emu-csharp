﻿using System;

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

        public uint HostIoAddr { get; set; } = 0x80001000;

        public uint Pc
        {
            set => processor.Core.Pc32 = value;
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
                if (GetHostIoValue() != 0)
                {
                    break;
                }

                processor.ProcessCycle();
            }

            PrintHostIoValue(GetHostIoValue());
        }

        private uint GetHostIoValue() => bus.ReadUInt32(HostIoAddr);

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
