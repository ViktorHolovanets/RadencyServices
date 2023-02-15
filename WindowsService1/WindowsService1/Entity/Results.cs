using CsvHelper;
using RadencyService.Entity.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadencyService.Entity
{
    public class Results
    {
        public List<Result> results { get; }
        public Results()
        {
            results = new List<Result>();
        }
        public void addResut(string str)
        {
            var line = str.Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
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
                SingletonLog.GetInstance().metaLog.setField(OperationLog.parsed_lines);
            }
            catch (Exception ex)
            {
                SingletonLog.GetInstance().metaLog.setField(OperationLog.found_errors);
            }
        }
        public void addResut(CsvReader csvReader)
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
        }
    }
}
