using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsService1;

namespace RadencyService.Entity
{
    public class Result
    {
        public string city { get; set; }
        public List<Service> services { get; set; }
        public decimal total => services.Sum(s => s.total);

        public Result(string city)
        {
            this.city = city;
            services = new List<Service>();

        }
        public void addService(Service services)
        {
            this.services.Add(services);
        }
    }
}
