using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Interfaces
{
    public interface IFeedbackService
    {
        IEnumerable<FeedbackModel> GetFeedbacksForItem(int id);
        FeedbackModel GetFeedback(int id);
        float? GetAverageMark(int id);
        void PostFeedback(FeedbackModel feedback);
        void EditFeedback(FeedbackModel feedback);
        void DeleteFeedback(int id);
    }
}
