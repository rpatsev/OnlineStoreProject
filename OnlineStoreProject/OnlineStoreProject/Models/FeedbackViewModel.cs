using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class FeedbackViewModel
    {
        public FeedbackViewModel() { }
        public FeedbackViewModel(FeedbackModel feedback)
        {
            FeedbackId = feedback.FeedbackId;
            Text = feedback.Text;
            Points = feedback.Points;
            ProductId = feedback.ProductId;
            UserId = feedback.UserId;
        }
        public int FeedbackId { get; set; }
        public string Text { get; set; }
        public float? Points { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}