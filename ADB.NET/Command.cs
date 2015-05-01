using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ADB.NET
{
    class Command
    {
        private byte[] command;
        private byte[] content;
        private byte[] status = new byte[4];
        private byte[] length = new byte[4];

        private static Encoding encoding = Encoding.ASCII;

        private static byte[] OKAY = encoding.GetBytes("OKAY");
        private static byte[] FAIL = encoding.GetBytes("FAIL");

        public Command(String command)
        {
            this.command = encoding.GetBytes(command.Length.ToString("X4") + command + "\n");
        }

        public CommandResponse Execute()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(Adb.HOST, Adb.PORT);

                socket.Send(command);

                socket.Receive(status);
                socket.Receive(length);

                content = new byte[int.Parse(encoding.GetString(length), NumberStyles.HexNumber)];

                socket.Receive(content);

                if (status.SequenceEqual(OKAY))
                {
                    return new CommandResponse(content);
                }
                else
                {
                    throw new Exception(encoding.GetString(content));
                }
            }
        }
    }
}
