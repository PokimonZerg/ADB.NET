using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADB.NET.TEST
{
    [TestClass]
    public class MiscTest
    {
        [TestInitialize]
        public void StartAdbDaemon()
        {
            Adb.StartServer(@"C:\SDK\android\platform-tools\adb.exe");
        }

        [TestMethod]
        public void Version()
        {
            Assert.AreEqual<String>(Adb.Version(), "32");
        }

        [TestCleanup]
        public void StopAdbDaemon()
        {
            Adb.KillServer();
        }
    }
}
