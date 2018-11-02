using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Entities
{
    public class ClientProfile
    {
        [ForeignKey("ApplicationUser")]
        [Key]
        public string Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? BirthDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RegisteredAt { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ClientProfile()
        {
            Orders = new List<Order>();
            Feedbacks = new List<Feedback>();
        }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
