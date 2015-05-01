using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADB.NET.TEST
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void StartServer()
        {
            Adb.StartServer(@"C:\SDK\android\platform-tools\adb.exe");
        }

        [TestMethod]
        public void Version()
        {
            Assert.AreEqual<String>(Adb.Version(), "32");
        }

        [TestMethod]
        public void Connect()
        {
            Adb.Connect("192.168.56.101");
        }

        [TestMethod]
        public void ConnectPort()
        {
            Adb.Connect("192.168.56.101", 5555);
        }

        [TestMethod]
        public void Disconnect()
        {
            Adb.Disconnect("192.168.56.101");
        }

        [TestMethod]
        public void DisconnectPort()
        {
            Adb.Disconnect("192.168.56.101", 5555);
        }

        [TestMethod]
        public void KillServer()
        {
            Adb.KillServer();
        }
    }
}
