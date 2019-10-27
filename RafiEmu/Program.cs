using System;
using CommandLine;

namespace RafiEmu
{
    public class Program
    {
        private static readonly System system = new System();

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
