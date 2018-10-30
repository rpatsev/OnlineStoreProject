using AutoMapper;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IProductService service;

        public ProductController(IProductService _service)
        {
            this.service = _service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProductModel, ProductViewModel>()).CreateMapper();
            IEnumerable<ProductViewModel> products = mapper.Map<IEnumerable<ProductModel>, List<ProductViewModel>>(service.GetAllProducts());
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Create(ProductViewModel product)
        {
            ProductModel productModel = new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Country = product.Country,
                Manufacturer = product.Manufacturer,
                Alcohol = product.Alcohol,
                Volume = product.Volume,
                InStock = product.InStock,
                ImagePath = product.ImagePath,
                Price = product.Price,
                Category = service.GetCategory(product.CategoryId),
            };
            service.SaveProduct(productModel);

        }
        [HttpPost]
        public void Edit(int id, ProductViewModel product)
        {

            ProductModel productToUpdate = service.GetProduct(id);
            productToUpdate.Name = product.Name;
            productToUpdate.Country = product.Country;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Manufacturer = product.Manufacturer;
            productToUpdate.InStock = product.InStock;
            productToUpdate.Volume = product.Volume;
            productToUpdate.Category = service.GetCategory(product.CategoryId);
            service.UpdateProductData(id, productToUpdate);
        }

        [HttpPost]
        public void Remove(int id)
        {
            var productToDelete = service.GetProduct(id);
            if(productToDelete == null)
            {
                throw new Exception("Could not delete product with id: "+ id );
            }
            service.DeleteProductById(id);
        }

        public ActionResult GetProductData(int id)
        {
            var product = service.GetProduct(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImageForm()
        {
            SelectList products = new SelectList(service.GetAllProducts(), "ProductId", "Name");
            ViewBag.Products = products;
            return View();
        }

        public ActionResult UploadImage(int productId, HttpPostedFileBase upload)
        {
            if(upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                string filePath = "~/Files/" + fileName;
                upload.SaveAs(Server.MapPath(filePath));
                var product = service.GetProduct(productId);
                product.ImagePath = filePath;
                product.Category = service.GetCategory(product.CategoryId);
                service.UpdateProductData(productId, product);             
            }
            return RedirectToAction("Index");
        }
    }
}
