using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class ProductViewModel
    {
        public ProductViewModel(){ }
        public ProductViewModel(ProductModel product)
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
        public CategoryViewModel Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public double Volume { get; set; }
        public double? Alcohol { get; set; }
        public bool InStock { get; set; }
        public decimal Price { get; set; }
    }
}