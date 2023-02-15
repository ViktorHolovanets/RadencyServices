using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsService1;
using static System.Net.Mime.MediaTypeNames;

namespace RadencyService.Entity.Watcher
{
    public class WatcherTXT : BaseWatcher
    {
        string pathWrite;
        public WatcherTXT(string path, string pathWrite) : base(path, "*.txt")
        {
            this.pathWrite = pathWrite;
        }

        protected override void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string line = "";
            using (StreamReader sr = new StreamReader(e.FullPath))
            {
                Results results = new Results();
                while ((line = sr.ReadLine()) != null)
                {
                    results.addResut(line.Trim());
                }
                results.jsonSerializerResults(pathWrite, e.Name);
            }
        }

        protected override void Watcher_Created(object sender, FileSystemEventArgs e)
        {
           
        }

        protected override void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
        }
    }
}
