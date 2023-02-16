using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
           string pathA = ConfigurationManager.AppSettings["pathA"].ToString();
           string pathB = ConfigurationManager.AppSettings["pathB"].ToString();
            if (string.IsNullOrEmpty(pathA) || string.IsNullOrEmpty(pathB))
                return;
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1(pathA, pathB)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
