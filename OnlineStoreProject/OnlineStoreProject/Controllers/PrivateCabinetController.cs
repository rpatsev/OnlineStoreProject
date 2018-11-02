using MailChimp.Ecomm;
using Microsoft.AspNet.Identity;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Controllers
{
    [Authorize]
    public class PrivateCabinetController : Controller
    {
        private IAuthService authService;
        private IOrderService orderService;
        private IFeedbackService feedbackService;
        private IProductService productService;

        public PrivateCabinetController(
            IAuthService _authService,
            IOrderService _orderService,
            IFeedbackService _feedbackService,
            IProductService _productService)
        {
            this.authService = _authService;
            this.orderService = _orderService;
            this.feedbackService = _feedbackService;
            this.productService = _productService;
        }
        public ActionResult Index()
        {
            var userdata = GetPersonalData();
            return View(userdata);
        }

        public PersonalDataViewModel GetPersonalData()
        {
            var userId = this.User.Identity.GetUserId();
            var userdata = authService.GetUserData(userId);
            return new PersonalDataViewModel(userdata);
        }

        public ActionResult Edit()
        {
            var userdata = GetPersonalData();
            return View(userdata);
        }

        public ActionResult GetOrdersByUser()
        {
            var userId = this.User.Identity.GetUserId();
            var orders = orderService.GetOrdersByUser(userId).AsEnumerable();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFeedbacksByUser()
        {
            var userId = this.User.Identity.GetUserId();
            var feedbacks = feedbackService.GetFeedbacksByUser(userId);
            return Json(feedbacks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubmitChanges(PersonalDataViewModel model)
        {
            string userId = this.User.Identity.GetUserId();
            UserModel userModel = authService.GetUserData(userId);
            userModel.Id = userId;
            userModel.Address = model.Address;
            userModel.BirthDate = model.BirthDate;
            userModel.City = model.City;
            userModel.PhoneNumber = model.PhoneNumber;
            authService.EditPersonalData(userModel);
            return RedirectToAction("Index");
        }


    }
}