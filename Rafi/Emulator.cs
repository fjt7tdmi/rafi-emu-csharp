using System;

namespace Rafi
{
    public class Emulator
    {
        private readonly Memory memory = new Memory();

        internal Core Cpu { get; } = new Core();

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }
    }
}
