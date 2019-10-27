using System;
using CommandLine;

namespace RafiEmu
{
    internal class CommandLineOption
    {
        [Option('c', "cycle", Required = false, HelpText = "Number of emulation cycles")]
        public int Cycle { get; set; }

        [Option("gdb", Required = false, HelpText = "TCP port number of gdbserver")]
        public int GdbPort { get; set; }

        [Option('l', "load", Required = true, HelpText = "Binary file that is loaded to memory")]
        public string Load { get; set; }
    }
}
