using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class OrderItemViewModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int OrderItemId { get; set; }
        public int Amount { get; set; }
        public decimal Sum { get; set; }
    }
}