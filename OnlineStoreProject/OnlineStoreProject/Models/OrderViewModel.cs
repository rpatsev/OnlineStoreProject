using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal Sum { get; set; }
        public string Username { get; set; }
    }
}