using System;
using System.Collections.Generic;
using System.Text;

namespace DausterCustomer.Models
{
    public class PaymentMethods
    {
        public int id { get; set; }
        public string name { get; set; }
        public string card { get; set; }
        public string icon { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int type_cards_id { get; set; }
    }
}
