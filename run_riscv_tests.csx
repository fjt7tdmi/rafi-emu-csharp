using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

var rafiEmu = Path.GetFullPath("RafiEmu/bin/Debug/netcoreapp3.0/RafiEmu.exe");

var dir = "rafi-prebuilt-binary";
if (!Directory.Exists(dir))
{
    Console.Error.WriteLine($"Directory '{dir}' is not found.");
    Environment.Exit(1);
}

var exitCodes = new List<int>();

using (var stream = File.OpenRead("riscv_tests.config.txt"))
using (var reader = new StreamReader(stream))
{
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        if (string.IsNullOrWhiteSpace(line))
        {
            break;
        }

        var file = line.TrimEnd() + ".bin";
        var path = Path.Combine(dir, "riscv-tests", "isa", file);
        var xlen = line.StartsWith("rv32") ? 32 : 64;
        var arguments = $"-x {xlen} -c 1000 -l {path}";

        Console.WriteLine($"RafiEmu {arguments}");

        var process = new Process();
        process.StartInfo.Arguments = arguments;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.FileName = rafiEmu;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();
        process.StandardInput.Close();

        Console.WriteLine(process.StandardOutput.ReadToEnd());

        exitCodes.Add(process.ExitCode);
    }

    var success = exitCodes.Where(x => x == 0).Count();
    var failure = exitCodes.Where(x => x != 0).Count();

    Console.WriteLine($"{success} tests succeeded.");
    Console.WriteLine($"{failure} tests failed.");

    return failure;
}
