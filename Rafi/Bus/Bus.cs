using System;

namespace Rafi
{
    internal class Bus
    {
        private readonly Memory memory;

        public Bus(Memory memory)
        {
            this.memory = memory;
        }

        public uint ReadUInt32(uint addr) => memory.ReadUInt32(GetMemoryAddr(addr));

        private int GetMemoryAddr(uint addr) => (int)(addr - 0x80000000);
    }
}
