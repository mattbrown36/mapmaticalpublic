using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesEmulator
{
    public static class Util
    {
        //http://stackoverflow.com/questions/1469764/run-command-prompt-commands
        public static Process RunCommand(string cmd) {
            return RunProcess("cmd.exe", "/C " + cmd);
        }

        public static Process RunProcess(string processName, string args = "")
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            startInfo.FileName = processName;
            startInfo.Arguments = args;
            process.StartInfo = startInfo;
            process.Start();
            return process;
        }
    }
}
