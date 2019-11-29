using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Member
    {
        public Member()
        {
            Customer = new HashSet<Customer>();
            DeliveryCompany = new HashSet<DeliveryCompany>();
            Restaurant = new HashSet<Restaurant>();
        }

        public int MemberId { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
        public int MemberType { get; set; }

        public ICollection<Customer> Customer { get; set; }
        public ICollection<DeliveryCompany> DeliveryCompany { get; set; }
        public ICollection<Restaurant> Restaurant { get; set; }
    }
}
