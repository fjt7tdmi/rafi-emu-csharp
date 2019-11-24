using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Rafi
{
    internal class GdbSession
    {
        private const uint BreakInsn = 0x100073;

        private readonly Logger logger = new Logger("gdb");
        private readonly IDictionary<ulong, uint> memoryBackups = new Dictionary<ulong, uint>();

        private readonly Emulator emulator;

        public GdbSession(Emulator emulator)
        {
            this.emulator = emulator;
        }

        public void Process(StreamReader reader, StreamWriter writer)
        {
            logger.Info($"{nameof(GdbSession)} started.");

            try
            {
                while (true)
                {
                    var command = ReadCommand(reader);

                    SendAck(writer);

                    ProcessCommand(writer, command);

                    writer.Flush();
                }
            }
            catch (IOException)
            {
                // Normal connection close
            }
            finally
            {
                logger.Info($"{nameof(GdbSession)} finished.");
            }
        }

        private int CalcChecksum(string s) => CalcChecksum(s.ToCharArray().Select(x => (int)x));

        private int CalcChecksum(IEnumerable<int> values) => values.Sum() % 256;

        private string ConvertToHex(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
            {
                sb.Append($"{b:x2}");
            }

            return sb.ToString();
        }

        private string ConvertToHex(byte value) => ConvertToHex(new byte[1] { value });

        private string ConvertToHex(int value) => ConvertToHex(BitConverter.GetBytes(value));

        private string ConvertToHex(uint value) => ConvertToHex(BitConverter.GetBytes(value));

        private string ConvertToHex(long value) => ConvertToHex(BitConverter.GetBytes(value));

        private string ConvertToHex(ulong value) => ConvertToHex(BitConverter.GetBytes(value));

        private int ParseHex(string s)
        {
            int sum = 0;

            foreach (var c in s)
            {
                sum <<= 4;
                sum += ParseHex(c);
            }

            return sum;
        }

        private int ParseHex(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return (c - '0');
            }
            else if ('a' <= c && c <= 'f')
            {
                return (c - 'a') + 10;
            }
            else
            {
                throw new ArgumentException(nameof(c));
            }
        }

        private string ReadCommand(StreamReader reader)
        {
            var list = new List<int>();

            while (true)
            {
                var c = ReadChar(reader);
                if (c == '$')
                {
                    break;
                }
            }

            while (true)
            {
                var c = ReadChar(reader);

                if (c == '#')
                {
                    break;
                }

                list.Add(c);
            }

            var checksum = ReadChecksum(reader);
            if (checksum != CalcChecksum(list))
            {
                throw new IOException($"Checksum error. expected={CalcChecksum(list)} actual={checksum}");
            }

            var command = Encoding.UTF8.GetString(list.Select(x => (byte)x).ToArray());

            logger.Trace($"[recv] {command}");

            return command;
        }

        private int ReadChar(StreamReader reader)
        {
            var c = reader.Read();
            if (c == -1)
            {
                throw new IOException("Reached to the end of stream.");
            }
            else if (0 <= c && c <= 127)
            {
                return c;
            }
            else
            {
                throw new FormatException($"Invalid character of gdb protorol. (0x{c:x})");
            }
        }

        private int ReadChecksum(StreamReader reader)
        {
            var high = ReadChar(reader);
            var low = ReadChar(reader);

            var chars = new char[2] { (char)high, (char)low };

            return ParseHex(new string(chars));
        }

        private void ProcessCommand(StreamWriter writer, string command)
        {
            if (command.StartsWith("?"))
            {
                SendResponse(writer, "S05"); // 05: SIGTRAP
            }
            else if (command.StartsWith("H"))
            {
                // This emulator supports only thread 0, but always returns 'OK' for H command.
                SendResponse(writer, "OK");
            }
            else if (command.StartsWith("Z0"))
            {
                ProcessCommandInsertBreakPoint(writer, command);
            }
            else if (command.StartsWith("c"))
            {
                ProcessCommandContinue(writer, command);
            }
            else if (command.StartsWith("g"))
            {
                ProcessCommandReadReg(writer);
            }
            else if (command.StartsWith("m"))
            {
                ProcessCommandReadMemory(writer, command);
            }
            else if (command.StartsWith("q"))
            {
                ProcessCommandQuery(writer, command);
            }
            else if (command.StartsWith("s"))
            {
                ProcessCommandStep(writer, command);
            }
            else if (command.StartsWith("vCont?"))
            {
                SendResponse(writer, "vCont;c;s");
            }
            else if (command.StartsWith("z0"))
            {
                ProcessCommandRemoveBreakPoint(writer, command);
            }
            else
            {
                SendResponse(writer, "");
            }
        }

        private void ProcessCommandContinue(StreamWriter writer, string command)
        {
            emulator.Process(Emulator.StopCondition.Breakpoint);
            SendResponse(writer, "S05");
        }

        private void ProcessCommandReadReg(StreamWriter writer)
        {
            var sb = new StringBuilder();

            foreach (ulong value in emulator.Core.IntReg64.Values)
            {
                sb.Append(ConvertToHex(value));
            }

            sb.Append(ConvertToHex(emulator.Core.Pc64));

            SendResponse(writer, sb.ToString());
        }

        private void ProcessCommandReadMemory(StreamWriter writer, string command)
        {
            var args = command.Substring(1).Split(',');
            var addr = (ulong)ParseHex(args[0]);
            var length = ParseHex(args[1]);

            var buffer = new byte[length];

            try
            {
                emulator.Bus.Read(buffer, 0, length, addr);
            }
            catch (ArgumentOutOfRangeException)
            {
                Array.Fill<byte>(buffer, 0xcd);
            }

            SendResponse(writer, ConvertToHex(buffer));
        }

        private void ProcessCommandQuery(StreamWriter writer, string command)
        {
            var pos = command.IndexOf(';');
            var name = (pos != -1) ? command.Substring(0, pos) : command;

            switch (name)
            {
                case "qSupported":
                    SendResponse(writer, "PacketSize=1000");
                    break;
                case "qfThreadInfo":
                    SendResponse(writer, "mp01.01"); // pid=1, tid=1
                    break;
                case "qsThreadInfo":
                    SendResponse(writer, "l"); // End of thread list
                    break;
                case "qC":
                    SendResponse(writer, "QCp01.01"); // pid=1, tid=1
                    break;
                case "qAttached":
                    SendResponse(writer, "1");
                    break;
                default:
                    SendResponse(writer, "");
                    break;
            }
        }

        private void ProcessCommandStep(StreamWriter writer, string command)
        {
            emulator.ProcessCycle();
            SendResponse(writer, "T05");
        }

        private void ProcessCommandInsertBreakPoint(StreamWriter writer, string command)
        {
            var args = command.Substring(1).Split(',');
            var addr = (ulong)ParseHex(args[1]);

            Debug.Assert(!memoryBackups.ContainsKey(addr));

            var value = emulator.Bus.ReadUInt32(addr);
            memoryBackups.Add(addr, value);
            emulator.Bus.WriteUInt32(addr, BreakInsn);

            SendResponse(writer, "OK");
        }

        private void ProcessCommandRemoveBreakPoint(StreamWriter writer, string command)
        {
            var args = command.Substring(1).Split(',');
            var addr = (ulong)ParseHex(args[1]);

            Debug.Assert(memoryBackups.ContainsKey(addr));

            var value = memoryBackups[addr];
            emulator.Bus.WriteUInt32(addr, value);
            memoryBackups.Remove(addr);

            SendResponse(writer, "OK");
        }

        private void SendAck(StreamWriter writer)
        {
            writer.Write("+");
        }

        private void SendResponse(StreamWriter writer, string response)
        {
            var checksum = (byte)CalcChecksum(response);

            writer.Write($"${response}#{ConvertToHex(checksum):x2}");

            logger.Trace($"[send] {response}");
        }
    }
}
