using System;
using CommandLine;
using Rafi;

namespace RafiEmu
{
    public class Program
    {
        private static readonly Emulator emulator = new Emulator();

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOption>(args)
                .WithParsed(option =>
                {
                    emulator.LoadToMemory(option.Load);
                    emulator.Process(option.Cycle);
                });
        }
    }
}
