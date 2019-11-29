using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class FoodOrder
    {
        public FoodOrder()
        {
            Delivery = new HashSet<Delivery>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Delivery> Delivery { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
