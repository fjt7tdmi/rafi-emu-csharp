using System;
using CommandLine;

namespace RafiEmu
{
    internal class CommandLineOption
    {
        [Option('c', "cycle", Required = true, HelpText = "Number of emulation cycles")]
        public int Cycle { get; set; }

        [Option('l', "load", Required = true, HelpText = "Binary file that is loaded to memory")]
        public string Load { get; set; }

        [Option("pc", Required = false, HelpText = "Initial PC")]
        public uint Pc { get; set; } = 0x80000000;

        [Option("gdb", Required = false, HelpText = "TCP port number of gdbserver")]
        public int GdbPort { get; set; } = 0;
    }
}
