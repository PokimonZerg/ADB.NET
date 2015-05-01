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
            String resultStr = String.Format("{0}{1}\n", command.Length.ToString("X4"), command);
            this.command = Encoding.UTF8.GetBytes(resultStr);
        }

        public CommandResponse Execute()
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(Adb.HOST, Adb.PORT);

                socket.Send(command);

                socket.Receive(status);

                if (status.SequenceEqual(OKAY))
                {
                    socket.Receive(length);
                    
                    content = new byte[int.Parse(encoding.GetString(length), NumberStyles.HexNumber)];

                    socket.Receive(content);

                    return new CommandResponse(content);
                }
                else if (status.SequenceEqual(FAIL))
                {

                }
                else
                {
                    throw new Exception("Unknown server response");
                }
            }

            return null;
        }
    }
}
