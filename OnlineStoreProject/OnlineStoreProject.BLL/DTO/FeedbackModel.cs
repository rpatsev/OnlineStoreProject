using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.DTO
{
    public class FeedbackModel
    {
        public FeedbackModel() { }
        public FeedbackModel(Feedback feedback)
        {
            FeedbackId = feedback.FeedbackId;
            Text = feedback.Text;
            Points = feedback.Points;
            CreatedAt = feedback.CreatedAt;
            Product = new ProductModel(feedback.Product);
            ProductId = feedback.Product.ProductId;
        }
        public int FeedbackId { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public string Text { get; set; }
        public float? Points { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
