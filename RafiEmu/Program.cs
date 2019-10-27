using System;
using CommandLine;

namespace RafiEmu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOption>(args)
                .WithParsed(option =>
                {
                    Console.WriteLine($"Cycle: {option.Cycle}");
                    Console.WriteLine($"Load: {option.Load}");
                    Console.WriteLine($"GdbPort: {option.GdbPort}");
                });
        }
    }
}
