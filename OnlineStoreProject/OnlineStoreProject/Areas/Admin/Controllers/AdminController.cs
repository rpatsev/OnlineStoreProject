using OnlineStoreProject.Areas.Admin.Models;
using OnlineStoreProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStoreProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, superadmin")]
    public class AdminController : Controller
    {
        private IAuthService authService;
        public AdminController(IAuthService _service)
        {
            this.authService = _service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmailNotification(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.RegistrationLink = "http://10.10.43.8/Account/AdminRegister/";
                    model.Subject = "Admin invitation";
                    model.Sender = "alcostore.online@gmail.com";
                    new EmailController().SendEmail(model).Deliver();
                    return RedirectToAction("Success");
                }
                catch (Exception)
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetAdministratorsList()
        {
            var admins = authService.GetAllUsersByRole("admin");
            return Json(admins, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles ="superadmin")]
        public void Remove(string id)
        {
            authService.DenyAdminRights(id);
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }

}
