using System;
using System.Collections.Generic;

namespace DausterCustomer.Models
{
    public class Service
    {
        public int id { get; set; }
        public int time { get; set; }
        public int meters { get; set; }
        public double cost { get; set; }
        public double total_cost { get; set; }
        public int vehicle_id { get; set; }
        public int paymetMethod_id { get; set; }
        public List<Coordinates> coordinates { get; set; }
        public List<OtherCharges> lsOtherCharges { get; set; }
        public PickUp pickUp { get; set; }
        public Delivery delivery { get; set; }
    }

    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class PickUp
    {
        public string companyName { get; set; }
        public string name { get; set; }
        public string surnames { get; set; }
        public string phone { get; set; }
        public string packages { get; set; }
        public string shippingDescription { get; set; }
    }

    public class Delivery
    {
        public string companyName { get; set; }
        public string name { get; set; }
        public string surnames { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string arrivalDescription { get; set; }
    }

    public class OtherCharges
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string cost { get; set; }
    }
}
