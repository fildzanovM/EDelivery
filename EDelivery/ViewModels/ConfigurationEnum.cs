using EDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    public class ConfigurationEnum
    {
     
    }

    public enum WeekDays
    {
        Monday = 1,
        Tuesday = 2, 
        Wednesday =3, 
        Thursday = 4, 
        Friday = 5, 
        Saturday = 6, 
        Sunday = 7

    }

    public enum member_type
    {
        Customer = 8,
        Restaurant = 9, 
        DeliveryCompany = 10
    }

    public enum order_process
    {
        OrderConfirmation = 11,
        RestaurantPreparesOrder = 12,
        OrderIsReadyForDelivery = 13, 
        OrderIsOnItsWay = 14, 
        OrderDelivered = 15
    }

    
   
}
