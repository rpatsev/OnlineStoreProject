using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Interfaces;
using OnlineStoreProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Services
{
    public class FeedbackService : IFeedbackService
    {
        private IBaseRepository<Feedback> _feedbackRepo;
        private IBaseRepository<Product> _productRepo;
        private IUnitOfWork _usersRepo;
        public FeedbackService(IBaseRepository<Feedback> feedbackRepo, IBaseRepository<Product> prodRepo, IUnitOfWork usersRepo)
        {
            this._feedbackRepo = feedbackRepo;
            this._productRepo = prodRepo;
            this._usersRepo = usersRepo;
        }

        public FeedbackModel GetFeedback(int id)
        {
            var feedback = _feedbackRepo.GetWithInclude(i=>i.FeedbackId == id, s=>s.Product).SingleOrDefault();
            if (feedback == null)
            {
                throw new Exception("No feedback with such id!");
            }
            return new FeedbackModel(feedback);
        }

        public IEnumerable<FeedbackModel> GetFeedbacksForItem(int id)
        {
            var allFeedbacks = this.GetAllFeedbacks();
            return allFeedbacks.Where(f => f.ProductId == id).AsEnumerable();
        }

        public IEnumerable<FeedbackModel> GetFeedbacksByUser(string userid)
        {
            var allFeedbacks = this.GetAllFeedbacks();
            return allFeedbacks.Where(f => f.UserId == userid).AsEnumerable();
        }

        public float? GetAverageMark(int id)
        {
            return this.GetFeedbacksForItem(id).Select(p=>p.Points).Average();
        }

        public void PostFeedback(FeedbackModel feedback)
        {
            if (feedback == null)
            {
                throw new Exception("Could not save feedback");
            }
            var user = _usersRepo.UserManager.FindById(feedback.UserId);
            Feedback _feedback = new Feedback
            {
                Text = feedback.Text,
                CreatedAt = DateTime.Now,
                Points = feedback.Points,
                Product = _productRepo.Get(feedback.ProductId),
                ApplicationUser = user
            };
            _feedbackRepo.Add(_feedback);
        }
        public void DeleteFeedback(int id)
        {
            var feedback = _feedbackRepo.Get(id);
            if(feedback == null)
            {
                throw new Exception("No feedback with id: " + id);
            }
            _feedbackRepo.Delete(feedback);
        }

        public void EditFeedback(FeedbackModel feedback)
        {
            var feedbackToUpdate = _feedbackRepo.Get(feedback.FeedbackId);
            feedbackToUpdate.Text = feedback.Text;
            _feedbackRepo.Update(feedbackToUpdate);
        }

        private IEnumerable<FeedbackModel> GetAllFeedbacks()
        {
            List<FeedbackModel> feedbacks = new List<FeedbackModel>();
            IEnumerable<Feedback> feedbackEntities = _feedbackRepo.GetWithInclude(s => s.Product, s=>s.ApplicationUser);
            foreach(Feedback feedback in feedbackEntities)
            {
                FeedbackModel feedbackModel = new FeedbackModel(feedback);
                if(feedback.ApplicationUser != null)
                {
                    feedbackModel.UserId = feedback.ApplicationUser.Id;
                }
                feedbacks.Add(feedbackModel);
            }
            return feedbacks;
        }
    }
}
