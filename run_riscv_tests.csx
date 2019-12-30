using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

var filter = string.Empty;
if (Args.Count() > 0)
{
    filter = Args[0];
}

var rafiEmu = Path.GetFullPath("RafiEmu/bin/Debug/netcoreapp3.0/RafiEmu.exe");

var dir = "rafi-prebuilt-binary";
if (!Directory.Exists(dir))
{
    Console.Error.WriteLine($"Directory '{dir}' is not found.");
    Environment.Exit(1);
}

var exitCodes = new Dictionary<string, int>();

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

        var testName = line.TrimEnd();
        if (!testName.StartsWith(filter))
        {
            continue;
        }

        var file = testName + ".bin";
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

        exitCodes.Add(testName, process.ExitCode);
    }

    var success = exitCodes.Values.Where(x => x == 0).Count();
    var failure = exitCodes.Values.Where(x => x != 0).Count();

    Console.WriteLine("--");
    Console.WriteLine($"{success} tests succeeded.");
    Console.WriteLine($"{failure} tests failed.");
    Console.WriteLine("");
    Console.WriteLine("Failed tests:");

    foreach (var e in exitCodes)
    {
        if (e.Value != 0)
        {
            Console.WriteLine($"  {e.Key}");
        }
    }

    return failure;
}
