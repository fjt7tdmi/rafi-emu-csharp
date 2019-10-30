using System;
using CommandLine;
using Rafi;

namespace RafiEmu
{
    public class Program
    {
        private static readonly Emulator system = new Emulator();

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOption>(args)
                .WithParsed(option =>
                {
                    system.LoadToMemory(option.Load);
                });
        }
    }
}
