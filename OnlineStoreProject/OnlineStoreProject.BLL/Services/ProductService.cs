using AutoMapper;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreProject.BLL.Services
{
    public class ProductService : IProductService
    {
        private IBaseRepository<Product> _productRepo;
        private IBaseRepository<Category> _categoryRepo;
        private IBaseRepository<Feedback> _feedbackRepo;
        private IBaseRepository<OrderItem> _orderItemRepo;
        public ProductService(IBaseRepository<Product> _repoProd,
                                IBaseRepository<Category> _repoCat,
                                IBaseRepository<Feedback> _repoFeedback,
                                IBaseRepository<OrderItem> _orderItemFeedback)
        {
            this._productRepo = _repoProd;
            this._categoryRepo = _repoCat;
            this._feedbackRepo = _repoFeedback;
            this._orderItemRepo = _orderItemFeedback;
        }

        public void DeleteProductById(int id)
        {
            var product = _productRepo.Get(id);
            if(product == null)
            {
                throw new Exception("No product with id:" + id);
            }
            _productRepo.Delete(product);
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductModel>()).CreateMapper();
            IEnumerable<ProductModel> products = mapper.Map<IEnumerable<Product>, List<ProductModel>>(_productRepo.GetAll());
            return products; 
        }

        public IEnumerable<ProductModel> GetOrderedProductList(bool orderByPoints, bool orderByPurchase)
        {
            List<Product> _products = new List<Product>();
            List<int> productIdList = new List<int>();
            List<ProductModel> productsList = new List<ProductModel>();
            if (!orderByPoints && !orderByPurchase)
            {
                return this.GetAllProducts();
            }
            else if (orderByPoints)
            {
                productIdList = _feedbackRepo.GetWithInclude(p => p.Product)
                    .GroupBy(p=>p.Product.ProductId)
                    .Select((group, index) => new {
                        Product = group.Key,
                        Points = group.Average(c=>c.Points)})
                    .OrderByDescending(c=>c.Points)
                    .Select(p=>p.Product).ToList();               
            }
            else if (orderByPurchase)
            {
                productIdList = _orderItemRepo.GetWithInclude(p => p.Product)
                    .GroupBy(p => p.Product.ProductId)
                    .Select((group, index) => new {
                        Product = group.Key,
                        OrdersNumber = group.Sum(c => c.Amount)})
                    .OrderByDescending(c => c.OrdersNumber)
                    .Select(p => p.Product).ToList();
            }
            foreach(var productId in productIdList)
            {
                ProductModel _product = new ProductModel(_productRepo.Get(productId));
                productsList.Add(_product);
            }
            return productsList;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryModel>()).CreateMapper();
            return mapper.Map<IEnumerable<Category>, List<CategoryModel>>(_categoryRepo.GetAll());
        }

        public ProductModel GetProduct(int id)
        {
            var product = _productRepo.Get(id);
            if(product == null)
            {
                throw new Exception("Product was not found!");
            }
            return new ProductModel(product);
        }

        public CategoryModel GetCategory(int id)
        {
            var category = _categoryRepo.Get(id);
            if(category == null)
            {
                throw new Exception("Category was not found");
            }
            return new CategoryModel(category);
   
        }

        public IEnumerable<ProductModel> GetProductsByCategory(int catid)
        {
            var products = _productRepo.GetAll().Where(c => c.CategoryId == catid);
            List<ProductModel> productModels = new List<ProductModel>();
            foreach(Product product in products)
            {
                productModels.Add(new ProductModel(product));
            }
            return productModels;
        }

        public void SaveProduct(ProductModel product)
        {
            Product productToSave = new Product
            {
                Name = product.Name,
                Country = product.Country,
                Manufacturer = product.Manufacturer,
                Alcohol = product.Alcohol,
                Volume = product.Volume,
                InStock = product.InStock,
                Price = product.Price,
                ImagePath = product.ImagePath,
                Description = product.Description,
                Category = _categoryRepo.Get(product.Category.CategoryId)
            };
            _productRepo.Add(productToSave);
        }

        public void SaveCategory(CategoryModel category)
        {
            var categoryToSave = new Category
            {
                Name = category.Name,
                Description = category.Description
            };
            _categoryRepo.Add(categoryToSave);
        }

        public void DeleteCategoryById(int id)
        {
            var category = _categoryRepo.Get(id);
            if (category == null)
            {
                throw new Exception("No category with id:" + id);
            }
            _categoryRepo.Delete(category);
        }

        public void UpdateCategory(int id, CategoryModel category)
        {
            var categoryToUpdate = _categoryRepo.Get(id);
            if (categoryToUpdate == null)
            {
                throw new Exception("No category with id:" + id);
            }
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            _categoryRepo.Update(categoryToUpdate);
        }

        public void UpdateProductData(int id, ProductModel product)
        {
            var productToUpdate = _productRepo.Get(id);
            if(productToUpdate == null)
            {
                throw new Exception("No product with id: " + id);
            }
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Country = product.Country;
            productToUpdate.Manufacturer = product.Manufacturer;
            productToUpdate.Price = product.Price;
            productToUpdate.Volume = product.Volume;
            productToUpdate.ImagePath = product.ImagePath;
            productToUpdate.InStock = product.InStock;
            productToUpdate.Category = _categoryRepo.Get(product.Category.CategoryId);
            _productRepo.Update(productToUpdate);
        }
    }
}
