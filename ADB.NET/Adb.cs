using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

            if (!output.Contains("* daemon started successfully *\r") && output.Count() != 0)
                throw new Exception("Error occured while starting 'adb':" + output);
        }

        public static IEnumerable<AdbDevice> Devices
        {
            get
            {
                using (var link = new AdbLink())
                {
                    link.Send("host:devices");

                    var length = link.RecieveLength();
                    var response = link.Recieve(length);

                    foreach (var line in response.Split(new[] { '\n' }))
                    {
                        if (String.IsNullOrEmpty(line)) continue;

                        yield return new AdbDevice(line.Split(new[] { '\t' })[0]);
                    }
                }
            }
        }

        public static void Connect(String host)
        {
            using (var link = new AdbLink())
            {
                link.Send("host:connect:" + host);

                var length = link.RecieveLength();
                var response = link.Recieve(length);

                if (response.Contains("unable to connect"))
                    throw new Exception(response);
            }
        }

        public static void Connect(String host, int port)
        {
            using (var link = new AdbLink())
            {
                link.Send("host:connect:" + host + ":" + port);

                var length = link.RecieveLength();
                var response = link.Recieve(length);

                if (response.Contains("unable to connect"))
                    throw new Exception(response);
            }
        }

        public static void Disconnect(String host, int port)
        {
            using (var link = new AdbLink())
            {
                link.Send("host:disconnect:" + host + ":" + port);

                var length = link.RecieveLength();
                var version = link.Recieve(length);
            }
        }

        public static void Disconnect(String host)
        {
            using (var link = new AdbLink())
            {
                link.Send("host:disconnect:" + host);

                var length = link.RecieveLength();
                var version = link.Recieve(length);
            }
        }

        public static void KillServer()
        {
            using (var link = new AdbLink())
            {
                link.Send("host:kill");
            }
        }

        public static String Version()
        {
            using (var link = new AdbLink())
            {
                link.Send("host:version");

                var length = link.RecieveLength();
                var version = link.Recieve(length);

                return int.Parse(version, NumberStyles.HexNumber).ToString();
            }
        }
    }
}
