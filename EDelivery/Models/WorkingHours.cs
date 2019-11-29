using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class WorkingHours
    {
        public int WorkingHoursId { get; set; }
        public int DayOfWeek { get; set; }
        public int RestaurantId { get; set; }
        public TimeSpan TimeOpen { get; set; }
        public TimeSpan TimeClosed { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
