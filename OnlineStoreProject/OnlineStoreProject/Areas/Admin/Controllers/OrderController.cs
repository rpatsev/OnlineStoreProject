using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private IProductService productService;
        private IAuthService authService;

        public OrderController (IOrderService _orderService, IProductService _productService, IAuthService _authService)
        {
            this.orderService = _orderService;
            this.productService = _productService;
            this.authService = _authService;
        }
        public ActionResult Index()
        {
            List<OrderViewModel> ordersList = new List<OrderViewModel>();
            var orders = orderService.GetAllOrders().OrderByDescending(s => s.CreatedAt).Where(s=>s.Sum != 0).AsEnumerable();
            foreach (var order in orders)
            {
                OrderViewModel _order = new OrderViewModel
                {
                    CreatedAt = order.CreatedAt,
                    OrderId = order.OrderId,
                    Sum = order.Sum,
                    Username = (order.UserId != null) ? authService.GetUserData(order.UserId).UserName : null,
                };
                ordersList.Add(_order);
            }
            return View(ordersList);
        }

        public ActionResult Details(int id)
        {
            var orderItems = orderService.GetAllOrderItems().Where(s=>s.OrderId == id).AsEnumerable();
            List<OrderItemViewModel> orderItemModels = new List<OrderItemViewModel>();
            foreach(var orderItem in orderItems)
            {
                ProductModel productModel = productService.GetProduct(orderItem.ProductId);
                OrderItemViewModel _orderItem = new OrderItemViewModel
                {
                    Product = new ProductViewModel(productModel),
                    Amount = orderItem.Amount,
                    Sum = orderItem.Sum,
                    OrderId = orderItem.OrderId,
                };
                orderItemModels.Add(_orderItem);
            }

            return Json(orderItemModels, JsonRequestBehavior.AllowGet);
        }
        
        public void Remove([Bind(Include = "OrderId")] OrderViewModel _order)
        {
            int id = _order.OrderId;
            var order = orderService.GetOrder(id);
            if(order == null)
            {
                throw new Exception("Could not delete order with id" + id);
            }
            var orderItems = orderService.GetAllOrderItems().Where(s => s.OrderId == id);
            foreach(var orderItem in orderItems)
            {
                orderService.DeleteOrderItem(orderItem.OrderItemId);
            }
            orderService.DeleteOrder(id);
        }
    }
}