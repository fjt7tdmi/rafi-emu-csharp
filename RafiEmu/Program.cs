using System;
using CommandLine;
using Rafi;

namespace RafiEmu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOption>(args)
                .WithParsed(option =>
                {
                    var emulator = new Emulator();

                    emulator.Pc = option.Pc;
                    emulator.LoadToMemory(option.Load);
                    emulator.Process(option.Cycle);

                    if (option.GdbPort == 0)
                    {
                        // GDB server is disabled.
                        Environment.Exit(0);
                    }

                    using (var gdbServer = new GdbServer(emulator, option.GdbPort))
                    {
                        Console.WriteLine("GDB server is running.");
                        Console.WriteLine("Please input some characters to finish.");
                        Console.Read();
                    }
                });
        }
    }
}
