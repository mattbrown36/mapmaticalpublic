using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesEmulator
{
    class GoogleCloudDataStoreEmulator : EmulatedServiceRunner
    {
        public override ConsoleKey GetConsoleKey()
        {
            return ConsoleKey.S;
        }

        public override string GetServiceName()
        {
            return "Cloud Data Store - NoSql";
        }

        Process proc = null;

        protected override void StartService_Internal()
        {
            StopService_Internal();
            var javaProcs = Process.GetProcessesByName("java").Select(x => x.Id);
            proc = Util.RunProcess("gcloud", "beta emulators datastore start");
            Thread.Sleep(3000);
            proc = Process.GetProcessesByName("java").FirstOrDefault(x => !javaProcs.Contains(x.Id));
            if (proc == null) {
                Console.WriteLine("Associated Java Process Not Found");
            }
        }

        protected override void StopService_Internal()
        {
            if (proc != null && !proc.HasExited) {
                try
                {
                    proc.Kill();
                }
                finally {
                    proc = null;
                }
            }
        }
    }
}
