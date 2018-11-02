using ActionMailer.Net;
using ActionMailer.Net.Mvc;
using OnlineStoreProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OnlineStoreProject.Areas.Admin.Controllers
{
    public class EmailController : MailerBase
    {
        // GET: Admin/Email
        public EmailResult SendEmail(EmailModel model)
        {
            To.Add(model.Recepient);
            From = model.Sender;
            Subject = model.Subject;
            return Email("SendAdminAssignEmail", model);
        }        
    }
}