using SQLite;

namespace DausterCustomer.Models
{
    public class Country
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
