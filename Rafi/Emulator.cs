using System;

namespace Rafi
{
    public class Emulator
    {
        private readonly Memory memory = new Memory();

        internal Cpu Cpu { get; } = new Cpu();

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }
    }
}
