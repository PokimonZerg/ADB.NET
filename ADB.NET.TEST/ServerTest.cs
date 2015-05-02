using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADB.NET.TEST
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void StartStopServer()
        {
            Adb.StartServer(@"C:\SDK\android\platform-tools\adb.exe");

            Adb.KillServer();
        }
    }
}
