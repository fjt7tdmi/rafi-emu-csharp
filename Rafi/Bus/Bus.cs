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

        public void Read(byte[] buffer, int offset, int length, ulong addr)
        {
            memory.Read(buffer, offset, length, GetMemoryAddr(addr));
        }

        public byte ReadUInt8(ulong addr) => memory.ReadUInt8(GetMemoryAddr(addr));

        public ushort ReadUInt16(ulong addr) => memory.ReadUInt16(GetMemoryAddr(addr));

        public uint ReadUInt32(ulong addr) => memory.ReadUInt32(GetMemoryAddr(addr));

        public ulong ReadUInt64(ulong addr) => memory.ReadUInt64(GetMemoryAddr(addr));

        public void WriteUInt8(ulong addr, byte value) => memory.WriteUInt8(GetMemoryAddr(addr), value);

        public void WriteUInt16(ulong addr, ushort value) => memory.WriteUInt16(GetMemoryAddr(addr), value);

        public void WriteUInt32(ulong addr, uint value) => memory.WriteUInt32(GetMemoryAddr(addr), value);

        public void WriteUInt64(ulong addr, ulong value) => memory.WriteUInt64(GetMemoryAddr(addr), value);

        private int GetMemoryAddr(ulong addr) => (int)(addr - 0x80000000);
    }
}
