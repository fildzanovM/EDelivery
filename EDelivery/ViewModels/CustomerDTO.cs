using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //Create new Customer
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string Telephone { get; set; }
        public ICollection<CustomerLocation> Location { get; set; }
    }
    // Customer Location for Create new Customer
    public class CustomerLocation
    {
        public string City { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    //public class GetAllCustomers
    public class GetAllCustomers
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public ICollection<CustomerLocation> Location { get; set; }
    }


    //Edit Customer
    public class EditCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
    }

    public class DeleteCustomer
    {
        public int CustomerId { get; set; }
    }

    //Customer login info
    public class CustomerLogin
    {
        public int CustomerID { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
    }
    //Get Customer by ID
    public class GetCustomerbyID
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
