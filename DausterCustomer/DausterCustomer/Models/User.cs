using SQLite;
using System;

namespace DausterCustomer.Models
{
    public class User
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string surnames { get; set; }
        public DateTime birthday { get; set; }
        public string phone { get; set; }
        public int kind_persons_id { get; set; }
        public bool authorized { get; set; }
        public bool remember_me { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public object mistakes { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
