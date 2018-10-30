using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Entities
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Text { get; set; }
        public float? Points { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        public Product Product { get; set; }

    }
}
