using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Interfaces
{
    public interface IOrderService
    {
        void CreateOrderItem(int id, int orderid, int amount);
        void CreateOrder(string userId);
        IEnumerable<OrderItemModel> GetAllOrderItems();
        IEnumerable<OrderModel> GetAllOrders();
        OrderModel GetOrder(int id);
        OrderModel GetLastOrder();
        OrderItemModel GetOrderItem(int id);
        void DeleteOrder(int id);
        void DeleteOrderItem(int id);

    }
}
