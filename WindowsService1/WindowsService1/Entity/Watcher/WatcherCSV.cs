using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
                results.addResut(csvReader);
            }
            results.jsonSerializerResults(pathWrite, e.Name);
        }

        protected override void Watcher_Created(object sender, FileSystemEventArgs e)
        {

        }

        protected override void Watcher_Renamed(object sender, RenamedEventArgs e)
        {

        }
    }
}
