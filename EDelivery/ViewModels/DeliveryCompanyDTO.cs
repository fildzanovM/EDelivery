using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    //Create new Delivery Company
    public class DeliveryCompanyDTO
    {
        public int DeliveryCompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTelephone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPassword { get; set; }
     //   public ICollection<CompanyDriver> Drivers { get; set; }
    }

    //Create Company Driver for Delivery Company
    public class CompanyDriver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverTelephone { get; set; }
        public string DriverImage { get; set; }
    }

 
    //DeliveryCompany Login Info
    public class DeliveryCompanyLogin
    {
        public int DeliveryCompanyID { get; set; }
        public string DeliveryCompanyEmail { get; set; }
        public string DeliveryCompanyPassword { get; set; }
    }

    //Get Delivery Company by ID
    public class DeliveryCompanyByID
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTelephone { get; set; }
    }

    //EditDeliveryCompany
    public class EditDeliveryCompany
    {
        public string CompanyName { get; set; }
        public string CompanyTelephone { get; set; }
    }

    public class DeleteDeliveryCompany
    {
        public int DeliveryCompanyID { get; set; }
        public string DeliveryCompanyName { get; set; }
    }



    //GetDriverByID
    public class GetDriverByID
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    //EditDriver
    public class EditDriver
    {
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverTelephone { get; set; }
        public string DriverImage { get; set; }
    }

    public class CreateDriver
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverTelephone { get; set; }
        public string DriverImage { get; set; }
    }

    //Delete Driver
    public class DeleteDriver
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
