using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Customer
    {
        public Customer()
        {
            FoodOrder = new HashSet<FoodOrder>();
            Location = new HashSet<Location>();
        }

        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerTelephone { get; set; }
        public int MemberId { get; set; }

        public Member Member { get; set; }
        public ICollection<FoodOrder> FoodOrder { get; set; }
        public ICollection<Location> Location { get; set; }
    }
}
