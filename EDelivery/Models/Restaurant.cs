using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Food = new HashSet<Food>();
            Location = new HashSet<Location>();
            WorkingHours = new HashSet<WorkingHours>();
        }

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantImage { get; set; }
        public string RestaurantTelephone { get; set; }
        public int TypeId { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPassword { get; set; }
        public int CuisineTypeId { get; set; }
        public int MemberId { get; set; }

        public CuisineType CuisineType { get; set; }
        public Member Member { get; set; }
        public RestaurantType Type { get; set; }
        public ICollection<Food> Food { get; set; }
        public ICollection<Location> Location { get; set; }
        public ICollection<WorkingHours> WorkingHours { get; set; }
    }
}
