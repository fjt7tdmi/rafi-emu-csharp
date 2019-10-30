using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rafi
{
    internal class GdbSession
    {
        private readonly Emulator emulator;

        public GdbSession(Emulator emulator)
        {
            this.emulator = emulator;
        }

        public void Process(StreamReader reader, StreamWriter writer)
        {
            var command = ReadCommand(reader);

            SendAck(writer);

            ProcessCommand(writer, command);

            writer.Flush();
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

        private int ParseHex(char[] buffer, int offset, int length)
        {
            if (length % 2 != 0)
            {
                throw new ArgumentException(nameof(length));
            }

            int sum = 0;

            for (int i = offset; i < offset + length; i += 2)
            {
                sum <<= 8;
                sum += ParseHex(buffer[i]) << 4;
                sum += ParseHex(buffer[i + 1]);
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

            return Encoding.UTF8.GetString(list.Select(x => (byte)x).ToArray());
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

            return ParseHex(chars, 0, chars.Length);
        }

        private void ProcessCommand(StreamWriter writer, string command)
        {
            switch (command[0])
            {
                case 'g':
                    ProcessCommandReadReg(writer);
                    break;
                case 'm':
                    ProcessCommandReadMemory(writer, command);
                    break;
                case 'q':
                    ProcessCommandQuery(writer, command);
                    break;
                case 'H':
                    // This emulator supports only thread 0, but always returns 'OK' for H command.
                    SendResponse(writer, "OK");
                    break;
                case '?':
                    SendResponse(writer, "S05"); // 05: SIGTRAP
                    break;
                default:
                    SendResponse(writer, "");
                    break;
            }
        }

        private void ProcessCommandReadReg(StreamWriter writer)
        {
            // Only for RV32
            var sb = new StringBuilder();

            foreach (uint value in emulator.Cpu.IntReg.Values)
            {
                sb.Append(ConvertToHex(value));
            }

            sb.Append(ConvertToHex(emulator.Cpu.Pc));

            foreach (ulong value in emulator.Cpu.FpReg.Values)
            {
                sb.Append(ConvertToHex(value));
            }

            writer.Write(sb.ToString());
        }

        private void ProcessCommandReadMemory(StreamWriter writer, string command)
        {
            throw new NotImplementedException();
        }

        private void ProcessCommandQuery(StreamWriter writer, string command)
        {
            var pos = command.IndexOf(':');
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

        private void SendAck(StreamWriter writer)
        {
            writer.Write("+");
        }

        private void SendResponse(StreamWriter writer, string response)
        {
            var checksum = (byte)CalcChecksum(response);

            writer.Write($"${response}#{ConvertToHex(checksum):x2}");
        }
    }
}
