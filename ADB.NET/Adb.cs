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
            //process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd().Split(new[] { '\n' }).Except(new[] { "" });

            //ProcessOutput output = ProcessOutput.Parse(process);

            process.WaitForExit();

            if (!output.Contains("daemon started successfully") && output.Count() != 0)
                throw new Exception("Error occured while starting 'adb':" + output);
        }

        public static void KillServer()
        {

        }

        public static String Version()
        {
            var command = new Command("host:version");

            var response = command.Execute();

            return response.AsNumber().ToString();
        }
    }

    internal class CommandResult
    {

    }
}
