using System;
using System.IO;

namespace RafiEmu
{
    internal class Memory
    {
        private const int Capacity = 64 * 1024 * 1024;

        private byte[] body = new byte[Capacity];

        public void Load(string path)
        {
            using (var fileStream = File.Open(path, FileMode.Open))
            using (var reader = new BinaryReader(fileStream))
            {
                var buffer = reader.ReadBytes(Capacity);
                Buffer.BlockCopy(buffer, 0, body, 0, buffer.Length);
            }
        }
    }
}
