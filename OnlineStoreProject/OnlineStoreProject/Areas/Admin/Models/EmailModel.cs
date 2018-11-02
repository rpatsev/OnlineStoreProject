using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreProject.Areas.Admin.Models
{
    public class EmailModel
    {
        public string Subject { get; set; }
        public string Recepient { get; set; }
        public string Sender { get; set; }
        public string RegistrationLink { get; set; }
    }
}