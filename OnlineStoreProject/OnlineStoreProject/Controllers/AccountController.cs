using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Infrastructure;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.Models;

namespace OnlineStoreProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IAuthService authService;

        public AccountController(IAuthService _authService)
        {
            this.authService = _authService;
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserModel userModel = new UserModel { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await authService.Authentificate(userModel);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    string area = (claim.Claims.Where(c=>c.Value == "admin").SingleOrDefault() != null) ? "Admin" : string.Empty;
                    return RedirectToAction("Index", "Home", new { @area = area});

                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AdminRegister()
        {
            ViewBag.Role = "admin";
            return View("Register");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserModel userModel = new UserModel
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserName = model.UserName,
                    Address = model.Address,
                    BirthDate = model.BirthDate,
                    City = model.City,
                    Role = model.Role ?? "user",
                };
                OperationDetails operationDetails = await authService.Create(userModel);
                if (operationDetails.Succeded)
                {
                    string area = (model.Role == "admin") ? "Admin" : string.Empty;
                    return RedirectToAction("Index", "Home" , new { @area = area });
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await authService.SetInitialData(new UserModel
            {
                Email = "rpatsev@gmail.com",
                UserName = "rpatsev",
                Password = "87654321",
                City = "Kharkov",
                BirthDate = DateTime.Now.AddYears(-22),
                Role = "superadmin"
            }, new List<string> { "user", "admin", "superadmin" });
        }

        public string GetCurrentUserId()
        {
            return this.User.Identity.GetUserId();
        }

    }
}