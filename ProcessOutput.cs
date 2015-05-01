using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ADB.NET
{
    class ProcessOutput
    {
        private List<String> content;

        private ProcessOutput(List<String> content)
        {
            this.content = content.Except(new string[] {""}).ToList();
        }

        public static ProcessOutput Parse(Process process)
        {
            string output = process.StandardOutput.ReadToEnd();

            return new ProcessOutput(new List<String>(output.Split(new[] { '\n' })));
        }

        public bool IsEmpty()
        {
            return content.Count == 0;
        }

        public bool Contains(String pattern)
        {
            foreach (var line in content)
            {
                if (line.Contains(pattern))
                    return true;
            }

            return false;
        }
    }
}