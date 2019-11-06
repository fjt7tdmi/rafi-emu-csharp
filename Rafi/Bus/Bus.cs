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

        public byte ReadUInt8(uint addr) => memory.ReadUInt8(GetMemoryAddr(addr));

        public ushort ReadUInt16(uint addr) => memory.ReadUInt16(GetMemoryAddr(addr));

        public uint ReadUInt32(uint addr) => memory.ReadUInt32(GetMemoryAddr(addr));

        public void WriteUInt8(uint addr, byte value) => memory.WriteUInt8(GetMemoryAddr(addr), value);

        public void WriteUInt16(uint addr, ushort value) => memory.WriteUInt16(GetMemoryAddr(addr), value);

        public void WriteUInt32(uint addr, uint value) => memory.WriteUInt32(GetMemoryAddr(addr), value);

        private int GetMemoryAddr(uint addr) => (int)(addr - 0x80000000);
    }
}
