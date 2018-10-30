using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.DTO
{
    public class OrderModel
    {
        public OrderModel() { }
        public OrderModel(Order order)
        {
            OrderId = order.OrderId;
            CreatedAt = order.CreatedAt;
        }
        public int OrderId { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UserId { get; set; }
        public decimal Sum { get; set; }
    }
}
