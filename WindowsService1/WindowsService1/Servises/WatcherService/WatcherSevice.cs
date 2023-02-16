using RadencyService.Entity.Log;
using RadencyService.Entity.Watcher;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadencyService.Servises.WatcherService
{
    public class WatcherSevice
    {
        string pathA;
        string pathB;
        bool isRunning;
        List<BaseWatcher> watchers;
        public WatcherSevice(string pathA,string pathB)
        {
            this.pathA = pathA;
            this.pathB = pathB;
            isRunning = true;
            //pathA = ConfigurationManager.AppSettings["pathA"].ToString();
            //pathB = ConfigurationManager.AppSettings["pathB"].ToString();
            watchers = new List<BaseWatcher>();
        }
        public void createWatchers()
        {
            watchers.Add(new WatcherTXT(pathA,pathB));
            watchers.Add(new WatcherCSV(pathA, pathB));
        }
        public void Start()
        {
            watchers.ForEach(t => { Task.Run(t.Start); });
            while (isRunning)
            {
                if (DateTime.Now.ToString("HH:mm") == "23:59" && SingletonLog.GetInstance().metaLog.isSave())
                    SingletonLog.GetInstance().saveLog();
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watchers.ForEach(t => t.Stop());
            isRunning= false;
        }
        public void Pause()
        {
            watchers.ForEach(t => { Task.Run(t.Pause); });
        }
        public void Continue()
        {
            watchers.ForEach(t => { Task.Run(t.Continue); });
        }
    }
}
