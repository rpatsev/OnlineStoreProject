using OnlineStoreProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.DTO
{
    public class UserModel
    {
        public UserModel(ApplicationUser user)
        {
            Email = user.Email;
            UserName = user.UserName;
            BirthDate = user.ClientProfile.BirthDate;
            RegisteredAt = user.ClientProfile.RegisteredAt;
            City = user.ClientProfile.City;
            Address = user.ClientProfile.Address;
            Role = user.Roles.ToString();
        }
        public UserModel(){ }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
