using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Food
    {
        public Food()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public int RestaurantId { get; set; }
        public int CategoryId { get; set; }

        public FoodCategory Category { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
