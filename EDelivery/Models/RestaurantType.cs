using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class RestaurantType
    {
        public RestaurantType()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public ICollection<Restaurant> Restaurant { get; set; }
    }
}
