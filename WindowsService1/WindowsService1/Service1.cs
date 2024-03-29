﻿using RadencyService.Entity;
using RadencyService.Entity.Watcher;
using RadencyService.Servises.WatcherService;
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
        WatcherSevice watcherSevice;
        readonly string pathA;
        readonly string pathB;
        public Service1(string pathA, string pathB)
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
            this.pathA = pathA;
            this.pathB = pathB;
        }

        protected override void OnStart(string[] args)
        {
            watcherSevice = new WatcherSevice(pathA, pathB);
            watcherSevice.createWatchers();
            Thread loggerThread = new Thread(new ThreadStart(watcherSevice.Start));
            loggerThread.Start();
        }
        protected override void OnStop()
        {
            watcherSevice.Stop();
            Thread.Sleep(1000);
        }
        protected override void OnPause()
        {
            Task.Run(watcherSevice.Pause);
        }
        protected override void OnContinue() 
        {
            Task.Run(watcherSevice.Continue);
        }
    }
}
