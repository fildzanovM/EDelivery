using System;
using System.Collections.Generic;

namespace EDelivery.Models
{
    public partial class ConfigurationDelivery
    {
        public int ConfigurationValue { get; set; }
        public string ProgrameCode { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationDescription { get; set; }
    }
}
