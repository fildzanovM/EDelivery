using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class FoodCategory
    {
        public FoodCategory()
        {
            Food = new HashSet<Food>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Food> Food { get; set; }
    }
}
