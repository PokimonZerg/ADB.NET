using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.NET
{
    class CommandResponse
    {
        private byte[] content;

        private static Encoding encoding = Encoding.ASCII;

        public CommandResponse(byte[] content)
        {
            this.content = content;
        }

        public int AsNumber()
        {
            return int.Parse(encoding.GetString(content), NumberStyles.HexNumber);
        }
    }
}
