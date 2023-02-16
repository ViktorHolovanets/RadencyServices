using RadencyService.Entity.Log;
using RadencyService.Lib.File;
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
                    results.addRes<string>(results.results, line.Trim(), addResult);
                }
                if (results.results.Count == 0)
                    SingletonLog.GetInstance().metaLog.Operation(OperationLog.invalidFiles);
                else
                    Save.jsonSerializer<List<Result>>(pathWrite, e.Name, results.results);
                SingletonLog.GetInstance().metaLog.Operation(OperationLog.parsedFiles);
            }
        }
        public List<Result> addResult(List<Result> results, string str)
        {
            var line = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                Payer p = new Payer(line[0] + line[1],
                    decimal.Parse(line[5].Replace(" ", "").Replace('.', ',')),
                    DateTime.ParseExact(line[6].Replace(" ", ""), "yyyy-dd-MM", null),
                    long.Parse(line[7].Replace(" ", "")));
                var city = line[2].Replace("\u201C", "").Replace(" ", "");
                var res = results.FirstOrDefault(s => s.city == city);
                if (res == null)
                {
                    res = new Result(city);
                    results.Add(res);
                }
                var servis = res.services.FirstOrDefault(s => s.name == line[8].Replace(" ", ""));
                if (servis == null)
                {
                    servis = new Service(line[8].Replace(" ", ""));
                    res.addService(servis);
                }
                servis.addPayer(p);
                SingletonLog.GetInstance().metaLog.Operation(OperationLog.parsedLines);
            }
            catch (Exception ex)
            {
                SingletonLog.GetInstance().metaLog.Operation(OperationLog.foundErrors);
            }
            return results;
        }
        protected override void Watcher_Created(object sender, FileSystemEventArgs e)
        {

        }

        protected override void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
        }
    }
}
