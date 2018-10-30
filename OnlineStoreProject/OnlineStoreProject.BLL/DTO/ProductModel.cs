using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.DTO
{
    public class ProductModel
    {
        public ProductModel() { }
        public ProductModel(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Description = product.Description;
            ImagePath = product.ImagePath;
            Country = product.Country;
            Manufacturer = product.Manufacturer;
            Volume = product.Volume;
            Alcohol = product.Alcohol;
            InStock = product.InStock;
            Price = product.Price;
            CategoryId = product.CategoryId;
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public double Volume { get; set; }
        public double? Alcohol { get; set; }
        public bool InStock { get; set; }
        public decimal Price { get; set; }
        public ICollection<FeedbackModel> Feedbacks { get; set; }
    }
}
