using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class CuisineType
    {
        public CuisineType()
        {
            Restaurant = new HashSet<Restaurant>();
        }

        public int CuisineTypeId { get; set; }
        public string CuisineTypeName { get; set; }

        public ICollection<Restaurant> Restaurant { get; set; }
    }
}
