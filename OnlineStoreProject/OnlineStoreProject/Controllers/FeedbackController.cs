
using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace OnlineStoreProject.Controllers
{
    
    public class FeedbackController : Controller
    {
        private IFeedbackService feedbackService;
        private IProductService productService;
        private IAuthService authService;
        public FeedbackController(IFeedbackService _feedbackService,
            IProductService _prodService, IAuthService _authService)
        {
            this.feedbackService = _feedbackService;
            this.productService = _prodService;
            this.authService = _authService;
        }
        
        
        public ActionResult GetByProduct(int id)
        {
            List<FeedbackViewModel> feedbacks = new List<FeedbackViewModel>();
            IEnumerable<FeedbackModel> feedbackModels = feedbackService.GetFeedbacksForItem(id).AsEnumerable();
            foreach(FeedbackModel feedback in feedbackModels)
            {
                feedback.Product = productService.GetProduct(feedback.ProductId);
                FeedbackViewModel feedbackView = new FeedbackViewModel(feedback);
                if (feedback.UserId != null)
                {
                    feedbackView.Username = authService.GetUserData(feedback.UserId).UserName;
                }
                feedbacks.Add(feedbackView);
            }
            return Json(feedbacks, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public void Post(FeedbackViewModel feedback)
        {
            FeedbackModel feedbackModel = new FeedbackModel
            {
                Text = HttpUtility.HtmlEncode(feedback.Text),
                Points = feedback.Points,
                ProductId = feedback.ProductId,
                UserId = this.User.Identity.GetUserId(),
            };
            feedbackService.PostFeedback(feedbackModel);
        }

        [Authorize]
        public void Edit(FeedbackViewModel feedback)
        {
            var feedbackModel = feedbackService.GetFeedback(feedback.FeedbackId);
            feedbackModel.Text = feedback.Text;
            feedbackService.EditFeedback(feedbackModel);
        }

        [Authorize]
        public void Delete(int id)
        {
            var feedback = feedbackService.GetFeedback(id);
            if(feedback == null)
            {
                throw new Exception("No feedback with id: " + id);
            }
            feedbackService.DeleteFeedback(id);
        }

        public float? GetProductAverageMark(int id)
        {
            return feedbackService.GetAverageMark(id);
        }
    }
}