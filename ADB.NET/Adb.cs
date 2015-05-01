using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.NET
{
    public static class Adb
    {
        public static int PORT = 5037;
        public static String HOST = "localhost";

        public static void StartServer(String path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(path, "start-server");
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            Process process = Process.Start(startInfo);

            var output = process.StandardOutput.ReadToEnd().Split(new[] { '\n' }).Except(new[] { "" });

            process.WaitForExit();

            if (!output.Contains("daemon started successfully") && output.Count() != 0)
                throw new Exception("Error occured while starting 'adb':" + output);
        }

        public static void Connect(String host)
        {
            var command = new Command("host:connect:" + host);

            var response = command.Execute().AsString();

            if (response.Contains("unable to connect"))
                throw new Exception(response);
        }

        public static void Connect(String host, int port)
        {
            var command = new Command("host:connect:" + host + ":" + port);

            var response = command.Execute().AsString();

            if (response.Contains("unable to connect"))
                throw new Exception(response);
        }

        public static void Disconnect(String host, int port)
        {
            var command = new Command("host:disconnect:" + host + ":" + port);

            command.Execute();
        }

        public static void Disconnect(String host)
        {
            var command = new Command("host:disconnect:" + host);

            command.Execute();
        }

        public static void KillServer()
        {
            var command = new Command("host:kill");

            try
            {
                command.Execute();
            }
            catch
            {
                // this code always throw exception
            }
        }

        public static String Version()
        {
            var command = new Command("host:version");

            var response = command.Execute();

            return response.AsNumber().ToString();
        }
    }
}
