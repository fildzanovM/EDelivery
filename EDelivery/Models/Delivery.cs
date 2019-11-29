using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public string DeliveryName { get; set; }
        public int? OrderId { get; set; }

        public FoodOrder Order { get; set; }
    }
}
