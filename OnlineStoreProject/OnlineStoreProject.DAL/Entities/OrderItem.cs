using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public decimal Sum { get; set; }
        public int Amount { get; set; }
    }
}
