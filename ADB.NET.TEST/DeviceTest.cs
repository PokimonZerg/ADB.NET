using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADB.NET.TEST
{
    [TestClass]
    public class DeviceTest
    {
        [TestInitialize]
        public void StartAdbDaemon()
        {
            Adb.StartServer(@"C:\SDK\android\platform-tools\adb.exe");
            Adb.Connect("192.168.56.101", 5555);
        }

        [TestMethod]
        public void ListDevices()
        {
            Assert.AreEqual<int>(Adb.Devices.Count(), 1);
        }

        [TestMethod]
        public void DeviceShell()
        {
            var device = Adb.Devices.ElementAt(0);

            var output = device.Shell("ls");
        }

        [TestMethod]
        public void ConnectDisconnect()
        {
            Adb.Connect("192.168.56.101");
            Adb.Disconnect("192.168.56.101");
        }

        [TestMethod]
        public void ConnectDisconnectWuthPort()
        {
            Adb.Connect("192.168.56.101", 5555);
            Adb.Disconnect("192.168.56.101", 5555);
        }

        [TestCleanup]
        public void StopAdbDaemon()
        {
            Adb.KillServer();
        }
    }
}
