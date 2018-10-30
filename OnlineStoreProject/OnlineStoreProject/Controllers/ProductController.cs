using AutoMapper;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Controllers
{
    public class ProductController : Controller
    {
        private IProductService service;

        public ProductController (IProductService _service)
        {
            this.service = _service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsGroup(int id)
        {
            var products = service.GetProductsByCategory(id);
            List<ProductViewModel> itemsList = new List<ProductViewModel>();
            foreach (ProductModel product in products)
            {
                itemsList.Add(new ProductViewModel(product));
            }
            ViewBag.Category = service.GetCategory(id).Name;
            return View(itemsList);
        }

        public ActionResult Details(int id)
        {
            var product = service.GetProduct(id);
            ProductViewModel productViewModel = new ProductViewModel(product);
            ViewBag.Product = productViewModel;
            return View(productViewModel);
        }

        public ActionResult GetProductsInOrderByPoints()
        {
            return Json(service.GetOrderedProductList(true, false), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductsInOrderByPurchase()
        {
            return Json(service.GetOrderedProductList(false, true), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<ProductViewModel> GetAllProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductModel, ProductViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<ProductModel>, List<ProductViewModel>>(service.GetAllProducts());
        }
    }
}