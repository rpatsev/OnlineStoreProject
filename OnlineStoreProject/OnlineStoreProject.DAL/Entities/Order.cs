using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
