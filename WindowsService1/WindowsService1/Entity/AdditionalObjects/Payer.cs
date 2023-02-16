using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyService.Entity.AdditionalObjects
{
    public class Payer
    {
        public string name { get; set; }
        public decimal payment { get; set; }
        public DateTime dateTime { get; set; }
        public long accountNumber { get; set; }
        public Payer(string name, decimal payment, DateTime date, long number)
        {
            this.name = name;
            this.payment = payment;
            this.dateTime = date;
            this.accountNumber = number;
        }
    }
}
