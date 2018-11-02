using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Models
{
    public class PersonalDataViewModel
    {
        public PersonalDataViewModel(){ }
        public PersonalDataViewModel(UserModel model)
        {
            UserId = model.Id;
            Username = model.UserName;
            Email = model.Email;
            BirthDate = model.BirthDate;
            RegisteredAt = model.RegisteredAt;
            Address = model.Address;
            City = model.City;
            PhoneNumber = model.PhoneNumber;
        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        [Editable(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? RegisteredAt { get; private set; }
    }
}