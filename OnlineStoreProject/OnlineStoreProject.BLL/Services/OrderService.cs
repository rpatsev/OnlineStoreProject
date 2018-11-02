using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IBaseRepository<OrderItem> orderItemRepository;
        private IBaseRepository<Order> orderRepository;
        private IBaseRepository<Product> productRepository;
        private IUnitOfWork usersRepository;

        public OrderService(IBaseRepository<OrderItem> _orderItemRepo,
                            IBaseRepository<Order> _orderRepo,
                            IBaseRepository<Product> _prodRepo,
                            IUnitOfWork _usersRepo)
        {
            this.orderItemRepository = _orderItemRepo;
            this.orderRepository = _orderRepo;
            this.productRepository = _prodRepo;
            this.usersRepository = _usersRepo;
        }

        public void CreateOrderItem(int id, int orderid, int amount)
        {
            Product product = productRepository.Get(id);
            OrderItem orderItem = new OrderItem
            {
                Product = productRepository.Get(id),
                Amount = amount,
                Sum = product.Price * amount,
                Order = orderRepository.Get(orderid),                
            };
            orderItemRepository.Add(orderItem);
        }
        public void CreateOrder(string userId)
        {
            Order order = new Order()
            {
                CreatedAt = DateTime.Now,
                ApplicationUser = (!string.IsNullOrEmpty(userId)) ? usersRepository.UserManager.FindById(userId) : null,
            };
            orderRepository.Add(order);
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            List<OrderModel> ordersList = new List<OrderModel>();
            var orders = orderRepository.GetAll();
            foreach(var order in orders)
            {
                ordersList.Add(this.GetOrder(order.OrderId));
            }
            return ordersList;
        }

        public OrderModel GetOrder(int id)
        {
            Order order = orderRepository.GetWithInclude(s=>s.ApplicationUser).Where(s=>s.OrderId == id).SingleOrDefault();
            if(order == null)
            {
                throw new Exception("No order with this id" + id);
            }
            OrderModel orderModel = new OrderModel(order);
            orderModel.Sum = this.CalculateOrderSum(id);
            orderModel.UserId = (order.ApplicationUser != null) ? order.ApplicationUser.Id : null;
            return orderModel;
        }

        public OrderModel GetLastOrder()
        {
            return this.GetAllOrders().OrderByDescending(o => o.OrderId).FirstOrDefault();
        }

        public IEnumerable<OrderModel> GetOrdersByUser(string userId)
        {
            return this.GetAllOrders().Where(c=>c.UserId == userId);
        } 

        public OrderItemModel GetOrderItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItemModel> GetAllOrderItems()
        {
            List<OrderItemModel> orderItemsList = new List<OrderItemModel>();
            IEnumerable<OrderItem> orderItemEntities = orderItemRepository.GetWithInclude(s=>s.Order, s=>s.Product);
            foreach(var order in orderItemEntities)
            {
                OrderItemModel orderItem = new OrderItemModel
                {
                    Amount = order.Amount,
                    ProductId = order.Product.ProductId,
                    Sum = order.Sum,
                    OrderId = order.Order.OrderId,
                    OrderItemId = order.OrderItemId,
                };
                orderItemsList.Add(orderItem);
            }
            return orderItemsList;
        }

        private decimal CalculateOrderSum(int id)
        {
            return orderItemRepository.GetWithInclude(s=>s.Order).Where(s => s.Order.OrderId == id).Select(s => s.Sum).Sum();
        }

        public void DeleteOrder(int id)
        {
            var order = orderRepository.Get(id);
            if(order == null)
            {
                throw new Exception("No order with id " + id);
            }
            orderRepository.Delete(order);
        }

        public void DeleteOrderItem(int id)
        {
            var orderItem = orderItemRepository.Get(id);
            if (orderItem == null)
            {
                throw new Exception("No order item with id " + id);
            }
            orderItemRepository.Delete(orderItem);
        }
    }
}
