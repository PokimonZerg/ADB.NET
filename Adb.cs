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

        private static String path;

        public static String Path
        {
            get
            {
                if (String.IsNullOrEmpty(path))
                {
                    var ANDROID_HOME = Environment.GetEnvironmentVariable("ANDROID_HOME");

                    if (String.IsNullOrEmpty(ANDROID_HOME))
                        throw new Exception(@"Please set Path to adb or ANDROID_HOME environment variable before execute eny command.");

                    path = ANDROID_HOME + @"\platform-tools\adb.exe";
                }

                return path;
            }

            set
            {
                path = value;
            }
        }

        public static void StartServer()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Path, "start-server");
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            Process process = Process.Start(startInfo);

            ProcessOutput output = ProcessOutput.Parse(process);

            process.WaitForExit(MAX_TIMEOUT);

            if (!output.Contains("daemon started successfully") && !output.IsEmpty())
                throw new Exception("Error occured while starting 'adb'");
        }

        static int Main(string[] args)
        {
            Adb.StartServer();
            Adb.Version();
            return 0;
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
