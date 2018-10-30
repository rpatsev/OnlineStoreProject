using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public double Volume { get; set; }
        public double? Alcohol { get; set; }
        public bool InStock { get; set; }
        public decimal Price { get; set; }

        public ICollection<Feedback> Feedbacks { get; set;}
        public ICollection<OrderItem> OrderItems { get; set; }
        public Product()
        {
            Feedbacks = new List<Feedback>();
            OrderItems = new List<OrderItem>();
        }
    }
}
