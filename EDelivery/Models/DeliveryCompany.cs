using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class DeliveryCompany
    {
        public DeliveryCompany()
        {
            Driver = new HashSet<Driver>();
        }

        public int DeliveryCompanyId { get; set; }
        public string DeliveryCompanyName { get; set; }
        public string DeliveryCompanyTelephone { get; set; }
        public string DeliveryCompanyEmail { get; set; }
        public string DeliveryCompanyPassword { get; set; }
        public int MemberId { get; set; }

        public Member Member { get; set; }
        public ICollection<Driver> Driver { get; set; }
    }
}
