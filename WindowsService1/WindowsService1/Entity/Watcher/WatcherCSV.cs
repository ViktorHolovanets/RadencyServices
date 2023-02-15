using CsvHelper;
using CsvHelper.Configuration;
using RadencyService.Entity.Log;
using RadencyService.Lib.File;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RadencyService.Entity.Results;

namespace RadencyService.Entity.Watcher
{
    public class WatcherCSV : BaseWatcher
    {
        string pathWrite;
        public WatcherCSV(string path, string pathWrite) : base(path, "*.csv")
        {
            this.pathWrite = pathWrite;
        }

        protected override void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Results results = new Results();
           
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Comment = '#',
                AllowComments = true,
                Delimiter = ",",
            };

            var streamReader = File.OpenText(e.FullPath);
            var csvReader = new CsvReader(streamReader, csvConfig);

            while (csvReader.Read())
            {
                results.addRes<CsvReader>(results.results,csvReader, addResult);
            }
            Save.jsonSerializer<List<Result>>(pathWrite, e.Name, results.results);
            SingletonLog.GetInstance().metaLog.setField(OperationLog.parsed_files);
        }
        public List<Result> addResult(List<Result> results, CsvReader csvReader)
        {
            try
            {
                Payer p = new Payer(csvReader.GetField(0) + csvReader.GetField(1),
                    decimal.Parse(csvReader.GetField(5).Replace(" ", "").Replace('.', ',')),
                    DateTime.ParseExact(csvReader.GetField(6).Replace(" ", ""), "yyyy-dd-MM", null),
                    long.Parse(csvReader.GetField(7).Replace(" ", "")));
                var city = csvReader.GetField(2).Replace("\u201C", "").Replace(" ", "");
                var res = results.FirstOrDefault(s => s.city == city);
                if (res == null)
                {
                    res = new Result(city);
                    results.Add(res);
                }
                var servis = res.services.FirstOrDefault(s => s.name == csvReader.GetField(8).Replace(" ", ""));
                if (servis == null)
                {
                    servis = new Service(csvReader.GetField(8).Replace(" ", ""));
                    res.addService(servis);
                }
                servis.addPayer(p);
                SingletonLog.GetInstance().metaLog.setField(OperationLog.parsed_lines);
            }
            catch (Exception ex)
            {
                SingletonLog.GetInstance().metaLog.setField(OperationLog.found_errors);
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
