using Newtonsoft.Json;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineStoreProject.Controllers
{
    public class OrderItemController : Controller
    {
        private IOrderService orderService;
        private IProductService productService;
        private string ordersCookieKey = "orders";

        public OrderItemController (IOrderService _orderService,
                                IProductService _productService)
        {
            this.productService = _productService;
            this.orderService = _orderService;
        }

        public ActionResult Index()
        {
            List<ProductViewModel> productsList = new List<ProductViewModel>();
            if (HttpContext.Request.Cookies[ordersCookieKey] == null)
            {
                return View();
            }
            string ordersCookieString = HttpContext.Request.Cookies[ordersCookieKey].Value;
            Dictionary<int, int> orders = JsonConvert.DeserializeObject<Dictionary<int, int>>(ordersCookieString);
            foreach (var order in orders)
            {
                var product = productService.GetProduct(order.Key);
                productsList.Add(new ProductViewModel(product));
            }
            ViewBag.Products = productsList;
            return View();
        }

        public ActionResult Create(int id)
        {
            if (HttpContext.Request.Cookies[ordersCookieKey] != null)
            {
                string ordersCookieString = HttpContext.Request.Cookies[ordersCookieKey].Value;
                Dictionary<int, int> orders = JsonConvert.DeserializeObject<Dictionary<int, int>>(ordersCookieString);
                Dictionary<int, int> products = new Dictionary<int, int>();
                foreach (var order in orders)
                {
                    products.Add(order.Key, order.Value);
                    if (order.Key == id)
                    {
                        return RedirectToAction("Index");
                    }
                }
                products.Add(id, 1);
                HttpContext.Response.Cookies[ordersCookieKey].Value = JsonConvert.SerializeObject(products);
            }
            else
            {
                HttpContext.Response.Cookies[ordersCookieKey].Value = JsonConvert.SerializeObject(new Dictionary<int, int> { { id, 1 } });
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            string ordersCookieString = HttpContext.Request.Cookies[ordersCookieKey].Value;
            Dictionary<int, int> orders = JsonConvert.DeserializeObject<Dictionary<int,int>>(ordersCookieString);
            foreach (var order in orders.ToList())
            {
                if(order.Key == id)
                {
                    orders.Remove(order.Key);
                }
            }
            HttpContext.Response.Cookies[ordersCookieKey].Value = JsonConvert.SerializeObject(orders);
            return RedirectToAction("Index");
        }
    }

}