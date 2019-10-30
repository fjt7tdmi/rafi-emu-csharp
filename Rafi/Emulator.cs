using System;

namespace Rafi
{
    public class Emulator
    {
        private readonly Memory memory = new Memory();

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }
    }
}
