using System;
using System.Diagnostics;
using System.IO;

var envName = "RAFI_EMU";
var rafiEmu = Path.GetFullPath("RafiEmu/bin/Debug/netcoreapp3.0/RafiEmu.exe");

var dir = Environment.GetEnvironmentVariable(envName);
if (string.IsNullOrEmpty(dir))
{
    Console.Error.WriteLine($"Environment variable '{envName}' is not set.");
    Environment.Exit(1);
}
if (!Directory.Exists(dir))
{
    Console.Error.WriteLine($"Directory '{dir}' is not found.");
    Environment.Exit(1);
}

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
        var path = Path.Combine(dir, "work", "riscv-tests", file);
        var arguments = $"-c 1000 -l {path}";

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
    }
}
