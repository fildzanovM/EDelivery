using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public string CityName { get; set; }
        public string AddressName { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? RestaurantId { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
