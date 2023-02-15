using RadencyService.Entity.Watcher;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyService.Servises.WatcherService
{
    public class WatcherSevice
    {
        string pathA;
        string pathB;
        List<BaseWatcher> watchers;
        public WatcherSevice()
        {
            pathA = ConfigurationManager.AppSettings["pathA"].ToString();
            pathB = ConfigurationManager.AppSettings["pathB"].ToString();
            watchers= new List<BaseWatcher>();
        }
        public void createWatchers()
        {
            watchers.Add(new WatcherTXT(pathA,pathB));
            watchers.Add(new WatcherCSV(pathA, pathB));
        }
        public void Start()
        {
            watchers.ForEach(t => { Task.Run(t.Start); });

        }
        public void Stop()
        {
            watchers.ForEach(t => t.Stop());
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
