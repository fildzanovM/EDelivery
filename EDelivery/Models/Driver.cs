using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class Driver
    {
        public int DriverId { get; set; }
        public string DriverFirstName { get; set; }
        public int DeliveryCompanyId { get; set; }
        public string DriverLastName { get; set; }
        public string DriverImage { get; set; }
        public string DriverTelephone { get; set; }

        public DeliveryCompany DeliveryCompany { get; set; }
    }
}
