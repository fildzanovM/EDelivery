using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int? Quantity { get; set; }

        public Food Food { get; set; }
        public FoodOrder Order { get; set; }
    }
}
