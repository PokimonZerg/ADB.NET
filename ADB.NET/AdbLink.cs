using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ADB.NET
{
    internal class AdbLink : IDisposable
    {
        private static byte[] OKAY = Encoding.ASCII.GetBytes("OKAY");
        private static byte[] FAIL = Encoding.ASCII.GetBytes("FAIL");

        private Socket socket;

        public AdbLink()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.socket.Connect(Adb.HOST, Adb.PORT);
        }

        public void Send(String command)
        {
            var cmd = Encoding.ASCII.GetBytes(command.Length.ToString("X4") + command + "\n");

            this.socket.Send(cmd);

            var status = new byte[4];

            socket.Receive(status);

            if (status.SequenceEqual(FAIL))
            {
                var length = new byte[4];

                socket.Receive(length);

                var content = new byte[int.Parse(Encoding.ASCII.GetString(length), NumberStyles.HexNumber)];

                socket.Receive(content);

                throw new Exception(Encoding.ASCII.GetString(content));
            }
        }

        public int RecieveLength()
        {
            var length = new byte[4];

            socket.Receive(length);

            return int.Parse(Encoding.ASCII.GetString(length), NumberStyles.HexNumber);
        }

        public List<String> RecieveAll()
        {
            var result = new List<String>();
            var buffer = new byte[256];
            var content = new StringBuilder();
            
            for(var length = socket.Receive(buffer); length != 0; length = socket.Receive(buffer))
            {
                content.Append(Encoding.ASCII.GetString(buffer, 0, length));
            }

            return Regex.Split(content.ToString(), "\r\n").ToList();
        }

        public String Recieve(int length)
        {
            var content = new byte[length];

            socket.Receive(content);

            return Encoding.ASCII.GetString(content);
        }

        public void Dispose()
        {
            this.socket.Dispose();
        }
    }
}
