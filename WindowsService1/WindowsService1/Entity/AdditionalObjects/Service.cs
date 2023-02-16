using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyService.Entity.AdditionalObjects   
{
    public class Service
    {
        public string name { get; set; }
        public List<Payer> payers { get; set; }
        public decimal total => payers.Sum(s => s.payment);
        public Service(string name)
        {
            this.name = name;
            payers = new List<Payer>();
        }
        public void addPayer(Payer payer)
        {
            payers.Add(payer);
        }
    }
}
