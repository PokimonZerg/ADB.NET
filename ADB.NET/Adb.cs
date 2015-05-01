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
        private static int MAX_TIMEOUT = 12000;
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

            ProcessOutput output = ProcessOutput.Parse(process);

            process.WaitForExit(MAX_TIMEOUT);

            if (!output.Contains("daemon started successfully") && !output.IsEmpty())
                throw new Exception("Error occured while starting 'adb':" + output);
        }

        public static void KillServer()
        {

        }

        public static String Version()
        {
            var command = new Command("host:version");

            var z = command.Execute();

            return null;
        }
    }
}
