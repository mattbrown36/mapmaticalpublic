using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicesEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            var services = new List<EmulatedServiceRunner>() {
                new GoogleCloudDataStoreEmulator(),
            };

            Action printServices = () => {
                Console.WriteLine("Q => Quit");
                Console.WriteLine("C => Clear/Refresh");
                Console.WriteLine("A => Restart All");
                foreach (var service in services) {
                    var keyString = service.GetConsoleKey().ToString();
                    var msg = keyString + " => " + service.GetServiceName() + " : " + service.Status;
                    Console.WriteLine(msg);
                }
            };

            printServices();

            while (running) {
                var key = Console.ReadKey().Key;
                Console.WriteLine();
                if (key == ConsoleKey.Q)
                {
                    foreach (var service in services)
                    {
                        service.StopService();
                    }
                    running = false;
                }
                else if (key == ConsoleKey.C)
                {
                    Console.Clear();
                    printServices();
                }
                else if (key == ConsoleKey.A) {
                    foreach (var service in services) {
                        service.StopService();
                        service.StartService();
                    }
                }
                else
                {
                    var service = services.FirstOrDefault(x => x.GetConsoleKey() == key);
                    if (service != null)
                    {
                        service.Toggle();
                        printServices();
                    }
                }
            }
        }
    }
}
