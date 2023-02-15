using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadencyService.Entity.Watcher
{
    public abstract class BaseWatcher
    {
        protected FileSystemWatcher watcher { get; set; }
        bool isRunning;
        protected BaseWatcher(string path, string filter) 
        {         
            isRunning= true;
            watcher = new FileSystemWatcher(path,filter);
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        protected abstract void Watcher_Renamed(object sender, RenamedEventArgs e);
        protected abstract void Watcher_Changed(object sender, FileSystemEventArgs e);
        protected abstract void Watcher_Created(object sender, FileSystemEventArgs e);
        public  void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (isRunning)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            isRunning = false;
        }
        public void Pause()
        {
            watcher.EnableRaisingEvents = false;
        }
        public void Continue()
        {
            watcher.EnableRaisingEvents = true;
        }
    }
}
