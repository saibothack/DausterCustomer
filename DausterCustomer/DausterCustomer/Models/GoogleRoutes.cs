using System;
using System.Collections.Generic;
using System.Text;

namespace DausterCustomer.Models
{
    public class GoogleRoutes
    {
        public List<geocoded_waypoints> geocoded_waypoints { get; set; }
        public int routes { get; set; }
        public bool status { get; set; }
    }

    public class geocoded_waypoints {
        public bool geocoder_status { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    public class routes
    {
        public bool bounds { get; set; }

    }
}
