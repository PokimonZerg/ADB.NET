using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ADB.NET
{
    class Command
    {
        private byte[] command;

        public Command(String command)
        {
            String resultStr = String.Format("{0}{1}\n", command.Length.ToString("X4"), command);
            this.command = Encoding.UTF8.GetBytes(resultStr);
        }

        public CommandOutput Execute()
        {
            try
            {
                socket.Connect(Adb.HOST, Adb.PORT);

                socket.Send(command);

                var b = new byte[4];
                socket.Receive(b);


                var z = new byte[4];
                socket.Receive(z);
                String lenHex = Encoding.GetEncoding("ISO-8859-1").GetString(z);
                int len = int.Parse(lenHex, System.Globalization.NumberStyles.HexNumber);

                var  t = new byte[4];
                socket.Receive(t);
                lenHex = Encoding.GetEncoding("ISO-8859-1").GetString(t);
                len = int.Parse(lenHex, System.Globalization.NumberStyles.HexNumber);


                var gg = Encoding.GetEncoding("ISO-8859-1").GetString(b);
            }
            finally
            {
                socket.Close();
            }

            return new CommandOutput();
        }

        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
}
