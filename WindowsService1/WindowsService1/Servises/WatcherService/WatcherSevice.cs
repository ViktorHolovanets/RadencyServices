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
        public void Srart()
        {
            foreach (var item in watchers)
            {
                Task.Run(item.Start);
            }
        }
        public void Stop()
        {
            foreach (var item in watchers)
            {
                Task.Run(item.Start);
            }
        }
    }
}
