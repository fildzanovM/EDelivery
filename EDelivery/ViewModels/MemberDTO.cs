using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //Create Customer
    public class MemberDTO
    {       
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
    }

    //Create Restaurant
    public class RestaurantMember
    {
        public string RestaurantEmail { get; set; }
        public string RestaurantPassword { get; set; }
    }

    //Create DeliveryCompany
    public class DeliveryCompanyMember
    {
        public string DeliveryCompanyEmail { get; set; }
        public string DeliveryCompanyPassword { get; set; }
    }

    //Get Member by ID
    public class GetMemberById
    {
        public int MemberID { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
        public string MemberType { get; set; }
    }

    public class DeleteCustomerMember
    {
        public int MemberId { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
    }

    public class MemberTypeDto
    {
        public int TypeId { get; set; }
        public string MemberType { get; set; }
    }

}
