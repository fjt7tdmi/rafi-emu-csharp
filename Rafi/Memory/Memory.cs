using System;
using System.IO;

namespace Rafi
{
    internal class Memory
    {
        private const int Capacity = 64 * 1024 * 1024;

        private readonly byte[] body = new byte[Capacity];

        public void Load(string path)
        {
            using (var fileStream = File.Open(path, FileMode.Open))
            using (var reader = new BinaryReader(fileStream))
            {
                var buffer = reader.ReadBytes(Capacity);

                Buffer.BlockCopy(buffer, 0, body, 0, buffer.Length);
            }
        }

        public byte ReadUInt8(int addr) => body[addr];

        public ushort ReadUInt16(int addr) => BitConverter.ToUInt16(body, addr);

        public uint ReadUInt32(int addr) => BitConverter.ToUInt32(body, addr);

        public void WriteUInt8(int addr, byte value)
        {
            body[addr] = value;
        }

        public void WriteUInt16(int addr, ushort value)
        {
            var buffer = BitConverter.GetBytes(value);

            Buffer.BlockCopy(buffer, 0, body, addr, buffer.Length);
        }

        public void WriteUInt32(int addr, uint value)
        {
            var buffer = BitConverter.GetBytes(value);

            Buffer.BlockCopy(buffer, 0, body, addr, buffer.Length);
        }

    }
}
