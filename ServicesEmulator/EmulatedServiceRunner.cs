using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesEmulator
{
    abstract class EmulatedServiceRunner
    {
        public string Status { get; protected set; }
        bool started = false;

        /// <summary>
        /// Return the console key which starts/stops the service.
        /// </summary>
        /// <returns></returns>
        public abstract ConsoleKey GetConsoleKey();

        /// <summary>
        /// The name of the service.
        /// </summary>
        /// <returns></returns>
        public abstract string GetServiceName();

        /// <summary>
        /// Start up the service.
        /// </summary>
        protected abstract void StartService_Internal();

        /// <summary>
        /// Start up the service.
        /// </summary>
        protected abstract void StopService_Internal();

        public void StartService() {
            this.Status = "Starting";
            TryTo("Start Service", () => {
                StartService_Internal();
                this.Status = "Started";
                this.started = true;
            }); 
        }

        public void StopService() {
            this.Status = "Starting";
            TryTo("Stop Service", () => {
                StopService_Internal();
                this.Status = "Stopped";
                this.started = false;
            }); 
        }

        private void TryTo(string whatAreYouDoing, Action doThis) {
            try
            {
                doThis();
            }
            catch (Exception ex)
            {
                this.Status = "Error";
                Console.WriteLine();
                Console.WriteLine("Failed when '" + whatAreYouDoing + "': " + GetServiceName() ?? "Service-Name-NULL");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Trace: ");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                Console.WriteLine("C => Clear/Refresh");
            }
        }

        public void Toggle()
        {
            if (this.started)
            {
                StopService();
            }
            else {
                StartService();
            }
        }
    }
}
