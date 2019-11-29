using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //Create Location
    public class LocationDTO
    {
        public string CityName { get; set; }
        public string AddressName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    //Get Location by ID
    public class GetLocationById
    {
        public int LocationID { get; set; }
        public string CityName { get; set; }
        public string AddressName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    //Delete Location
    public class DeleteLocaiton
    {
        public int LocationId { get; set; }
    }

    //Edit Location
    public class EditLocation
    {
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
