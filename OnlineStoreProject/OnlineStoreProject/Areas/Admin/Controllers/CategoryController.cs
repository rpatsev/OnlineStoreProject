using AutoMapper;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Areas.Admin.Controllers
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
            return View();
        }

        public ActionResult GetAllCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryViewModel>()).CreateMapper();
            IEnumerable<CategoryViewModel> categories = mapper.Map<IEnumerable<CategoryModel>, List<CategoryViewModel>>(service.GetAllCategories());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategoriesSelectList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CategoryModel, CategoryListViewModel>()).CreateMapper();
            IEnumerable<CategoryListViewModel> categories = mapper.Map<IEnumerable<CategoryModel>, List<CategoryListViewModel>>(service.GetAllCategories());
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Create(CategoryViewModel category)
        {
            try
            {
                CategoryModel categoryModel = new CategoryModel
                {
                    Name = category.Name,
                    Description = category.Description
                };
                service.SaveCategory(categoryModel);
            }
            catch(Exception e)
            {
                throw new Exception("Could not create category");
            }
        }

        [HttpPost]
        public void Edit(int id, CategoryViewModel category)
        {
            try
            {
                CategoryModel categoryModel = service.GetCategory(id);
                categoryModel.Name = category.Name;
                categoryModel.Description = category.Description;
                service.UpdateCategory(id, categoryModel);
            }
            catch
            {
                throw new Exception("Could not edit category with id" + id);
            }
        }

        [HttpPost]
        public void Remove(int id)
        {
            var categoryToDelete = service.GetCategory(id);
            if(categoryToDelete == null)
            {
                throw new Exception("Could not delete category with id" + id);
            }
            service.DeleteCategoryById(id);
        }
    }
}
