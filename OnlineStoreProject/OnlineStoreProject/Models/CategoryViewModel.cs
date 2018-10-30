using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class CategoryViewModel
    {
 
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
        public CategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}