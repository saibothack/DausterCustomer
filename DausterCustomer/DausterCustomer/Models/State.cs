using SQLite;

namespace DausterCustomer.Models
{
    public class State
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
    }
}
