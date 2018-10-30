using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Controllers
{
    [Authorize]
    [AllowAnonymous]
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private IProductService productService;

        public OrderController(IOrderService _orderSevice, IProductService _productService)
        {
            this.productService = _productService;
            this.orderService = _orderSevice;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var ordersCookieString = HttpContext.Request.Cookies["orders"].Value;
            IDictionary<int, int> orders = JsonConvert.DeserializeObject<Dictionary<int, int>>(ordersCookieString);
            string userId = this.User.Identity.GetUserId();
            orderService.CreateOrder(userId);
            var newOrderId = orderService.GetLastOrder().OrderId;
            if(orders == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var order in orders)
            {
                orderService.CreateOrderItem(order.Key, newOrderId, order.Value);
            }
            if(HttpContext.Request.Cookies["orders"] != null)
            {
                HttpContext.Response.Cookies["orders"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Index");        
        }
    }
}
