﻿using CsvHelper;
using CsvHelper.Configuration;
using RadencyService.Entity.AdditionalObjects;
using RadencyService.Entity.Log;
using RadencyService.Lib.File;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;



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
            csvReader.Read();
            while (csvReader.Read())
            {
                results.addRes<CsvReader>(results.results,csvReader, addResult);
            }
            if (results.results.Count == 0)
                SingletonLog.GetInstance().metaLog.Operation(OperationLog.invalidFiles);
            else
                Save.jsonSerializer<List<Result>>(pathWrite, e.Name, results.results);
            SingletonLog.GetInstance().metaLog.Operation(OperationLog.parsedFiles);           
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
