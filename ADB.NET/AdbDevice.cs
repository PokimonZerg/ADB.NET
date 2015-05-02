using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.NET
{
    public class AdbDevice
    {
        public String Serial
        {
            get;
            private set;
        }

        internal AdbDevice(String serial)
        {
            this.Serial = serial;
        }

        public List<String> Shell(String command, params object[] args)
        {
            using (var link = new AdbLink())
            {
                link.Send("host:transport:" + Serial);
                link.Send("shell" + ":" + command + " " + String.Join(" ", args));

                return link.RecieveAll();
            }
        }
    }
}
