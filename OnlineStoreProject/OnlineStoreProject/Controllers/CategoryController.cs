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
    public class CategoryController : Controller
    {
        private IProductService service;
        public CategoryController(IProductService _service)
        {
            this.service = _service;
        }
        public ActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = this.GetAllCategories();
            return View(categories);
        }

        public ActionResult DisplayCategories()
        {
            IEnumerable<CategoryViewModel> categories = this.GetAllCategories();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<CategoryViewModel> GetAllCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<CategoryModel>, List<CategoryViewModel>>(service.GetAllCategories());
        }

    }
}