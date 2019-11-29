using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }

    public class LoginMember
    {
        public int MemberId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryCompanyId { get; set; }
        public int MemberType { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
    }
}
