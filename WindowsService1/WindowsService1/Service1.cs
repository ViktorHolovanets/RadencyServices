using RadencyService.Entity;
using RadencyService.Entity.Watcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        BaseWatcher watcher;
        BaseWatcher watcher1;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string pathA = ConfigurationManager.AppSettings["pathA"].ToString();
            string pathB = ConfigurationManager.AppSettings["pathB"].ToString();
           
            string configvalue2 = ConfigurationManager.AppSettings["pathB"].ToString();
            watcher = new WatcherTXT(pathA, pathB);
            watcher1 = new WatcherCSV(pathA, pathB);
            Thread loggerThread = new Thread(new ThreadStart(watcher.Start));
            Thread loggerThread1 = new Thread(new ThreadStart(watcher1.Start));
            loggerThread.Start();
            loggerThread1.Start();
        }
        protected override void OnStop()
        {
            watcher.Stop();
            watcher1.Stop();
            Thread.Sleep(1000);
        }
    }
}
