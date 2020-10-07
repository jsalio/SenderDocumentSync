using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace WindowsApiService
{
    static class Program
    {
        private static Service1 releaseService;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            UnityContainer container = new UnityContainer();
            UnityConfig.RegisterComponents(container);
            if (Environment.UserInteractive)
            {
                using (var service = new Service1())
                {
                    Console.Title = "Integration API Interactive Mode";
                    Console.WriteLine("Running Api");
                    container.BuildUp(service);
                    releaseService = service;
                    releaseService.OnDebug();
                    Console.ReadLine();
                    releaseService.Stop();
                }
            }
            else
            {
                releaseService = new Service1();
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    container.BuildUp(releaseService)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
