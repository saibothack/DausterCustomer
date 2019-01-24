using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DausterCustomer.Models
{
    public class Billing
    {
        public int countries_id { get; set; }
        public int states_id { get; set; }
        public string RFC { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string cp { get; set; }
        public string interior { get; set; }
        public string exterior { get; set; }
        public string street { get; set; }
        public string colony { get; set; }
        public string city { get; set; }
    }
}
