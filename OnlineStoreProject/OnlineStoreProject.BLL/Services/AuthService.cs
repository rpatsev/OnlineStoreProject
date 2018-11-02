using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStoreProject.BLL.DTO;
using OnlineStoreProject.BLL.Infrastructure;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.Security;

namespace OnlineStoreProject.BLL.Services
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork db;
        public AuthService(IUnitOfWork _db)
        {
            this.db = _db;
        }

        public async Task<ClaimsIdentity> Authentificate(UserModel userModel)
        {
            ClaimsIdentity claim = null;
            PasswordVerificationResult result = PasswordVerificationResult.Failed;
            ApplicationUser user = await db.UserManager.FindByEmailAsync(userModel.Email);
            if (user != null)
            {
                result = db.UserManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, userModel.Password);
                if(result == PasswordVerificationResult.Success)
                {
                    claim = await db.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return claim;
        }

        public async Task<OperationDetails> Create(UserModel userModel)
        {
            ApplicationUser user = await db.UserManager.FindByEmailAsync(userModel.Email);
            if(user == null)
            {
                user = new ApplicationUser
                {
                    Email = userModel.Email,
                    UserName = userModel.UserName,
                };
                var result = await db.UserManager.CreateAsync(user, userModel.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                await db.UserManager.AddToRoleAsync(user.Id, userModel.Role);
                ClientProfile clientProfile = new ClientProfile
                {
                    Id = user.Id,
                    Address = userModel.Address,
                    BirthDate = userModel.BirthDate,
                    City = userModel.City,
                    RegisteredAt = DateTime.Now,
                };
                db.ClientManager.Create(clientProfile);
                await db.SaveAsync();
                return new OperationDetails(true, "Registration completed successfully", "");
            }
            else
            {
                return new OperationDetails(false, "User with login " + userModel.UserName + " has already registered", "Username");
            }
        }

        public UserModel GetUserData(string userid)
        {
            var user = db.UserManager.FindById(userid);
            return new UserModel(user);
        }

        public void EditPersonalData(UserModel userModel)
        {
            ClientProfile clientProfile = db.ClientManager.Get(userModel.Id);
            clientProfile.Address = userModel.Address;
            clientProfile.City = userModel.City;
            clientProfile.PhoneNumber = userModel.PhoneNumber;
            clientProfile.BirthDate = userModel.BirthDate;
            db.ClientManager.Update(clientProfile);
        }

        public IEnumerable<UserModel> GetAllUsersByRole(string roleName)
        {
            string roleId = db.RoleManager.Roles.Where(c => c.Name == roleName).Select(c => c.Id).FirstOrDefault();
            IQueryable<ApplicationUser> selectedUsers = db.UserManager.Users.Where(x => x.Roles.Any(s => s.RoleId == roleId));
            List<UserModel> usersList = new List<UserModel>();
            foreach (ApplicationUser user in selectedUsers)
            {
                UserModel userModel = new UserModel();
                userModel.Id = user.Id;
                userModel.Email = user.Email;
                userModel.UserName = user.UserName;
                userModel.RegisteredAt = user.ClientProfile.RegisteredAt;
                usersList.Add(userModel);
            }
            return usersList;
        }

        public void DenyAdminRights(string userid)
        {
            var user = db.UserManager.FindById(userid);
            IdentityResult identityResult = db.UserManager.RemoveFromRole(userid, "admin");
        }

        public async Task SetInitialData(UserModel admin, List<string> roles)
        {
            foreach(string roleName in roles)
            {
                var role = await db.RoleManager.FindByNameAsync(roleName);
                if(role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await db.RoleManager.CreateAsync(role);
                }
            }
            await Create(admin); 
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
