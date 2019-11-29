using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDelivery.ViewModels
{
    public class OrderDTO
    {
    }

    public class Order_Process
    {
        public int OrderProcessId { get; set; }
        public string OrderProcess { get; set; }
    }

    public class CreateOrder
    {
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItemDTO> OrderItem { get; set; }
    }

    public class OrderItemDTO
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
    }

    public class GetOrderId
    {
        public int OrderId { get; set; }
    }


}
