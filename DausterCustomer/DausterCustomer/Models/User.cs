using SQLite;
using System;

namespace DausterCustomer.Models
{
    public class User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string surnames { get; set; }
        public DateTime birthday { get; set; }
        public string phone { get; set; }
        public int kind_persons_id { get; set; }
    }
}
