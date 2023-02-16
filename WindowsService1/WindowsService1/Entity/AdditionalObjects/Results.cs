using CsvHelper;
using RadencyService.Entity.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadencyService.Entity.AdditionalObjects
{
    public class Results
    {
        public List<Result> results { get; set; }

        public Results()
        {
            results = new List<Result>();
        }

        public void addRes<T>(List<Result> results, T message, Func<List<Result>, T, List<Result>> action)
        {
            this.results = action(results, message);
        }

    }
}
