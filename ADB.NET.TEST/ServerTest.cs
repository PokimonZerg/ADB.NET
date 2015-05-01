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
    }
}
