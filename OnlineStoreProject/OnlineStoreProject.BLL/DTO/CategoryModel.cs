using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.DTO
{
    public class CategoryModel
    {
        public CategoryModel(Category category)
        {
            CategoryId = category.CategoryId;
            Name = category.Name;
            Description = category.Description;
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public CategoryModel()
        {
            Products = new List<ProductModel>();
        }

    }
}
