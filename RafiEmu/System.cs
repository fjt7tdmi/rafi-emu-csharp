using System;
using CommandLine;

namespace RafiEmu
{
    internal class System
    {
        private readonly Memory memory = new Memory();

        public void LoadToMemory(string path)
        {
            memory.Load(path);
        }
    }
}
