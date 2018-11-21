using SQLite;

namespace DausterCustomer.Models
{
    public class KindPersons
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
