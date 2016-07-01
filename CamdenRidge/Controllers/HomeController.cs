using CamdenRidge.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CamdenRidge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact us with your thoughts, questions and suggestions.";
            var model = new ContactViewModel
            {
                sendToItemTypes = new List<SelectListItem>()
            };
            model.sendToItemTypes.Add(new SelectListItem() { Text = Common.Constants.BoardMembers, Value = Common.Constants.BoardMembers });
            model.sendToItemTypes.Add(new SelectListItem() { Text = Common.Constants.AECCMembers, Value = Common.Constants.AECCMembers });
            model.sendToItemTypes.Add(new SelectListItem() { Text = Common.Constants.Management, Value = Common.Constants.Management });
            model.sendToItemTypes.Add(new SelectListItem() { Text = Common.Constants.Treasurer, Value = Common.Constants.Treasurer });
            model.sendToItemTypes.Add(new SelectListItem() { Text = Common.Constants.Secretary, Value = Common.Constants.Secretary });
            model.Sent = false;
            if (Request.IsAuthenticated)
            {
                model.Email = User.Identity.Name;
                var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(store);
                ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

                model.Address = user.Address;
                model.Name = user.Name;
            }

            return View(model);
        }

        [HttpPost]

        public ActionResult Contact(ContactViewModel model)
        {
            List<string> distro = new List<string>();
            switch (model.SendTo)
            {
                case Common.Constants.BoardMembers:
                    distro = System.Configuration.ConfigurationManager.AppSettings["BoardMembersDistro"].Split(',').ToList();
                break;
                case Common.Constants.AECCMembers:
                    distro = System.Configuration.ConfigurationManager.AppSettings["AECCMembersDistro"].Split(',').ToList();
                    break;
                case Common.Constants.Management:
                    distro = System.Configuration.ConfigurationManager.AppSettings["ManagementDistro"].Split(',').ToList();
                    break;
                case Common.Constants.Treasurer:
                    distro = System.Configuration.ConfigurationManager.AppSettings["TreasurerDistro"].Split(',').ToList();
                    break;
                case Common.Constants.Secretary:
                    distro = System.Configuration.ConfigurationManager.AppSettings["SecretaryDistro"].Split(',').ToList();
                    break;
            }


            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGridApiKey"];
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email(model.Email);
            String subject = "Message from " + model.Name;
            Email to = new Email(distro.FirstOrDefault());
            string body = "<html><head><title>Message from Camden Ridge Website</title></head><body><table><tr><td>" +
                "Name: " + model.Name + "<br>Address: " + model.Address + "<br>Phone: " + model.Phone + "<br>Message: " + model.Text +
                "</td></tr></table></body></html>";
            Content content = new Content("text/html", body);
            Mail mail = new Mail(from, subject, to, content);
            foreach (string emailAddress in distro.Skip(1))
            {
                Email email = new Email(emailAddress);
                mail.Personalization[0].AddTo(email);
            }

            String ret = mail.Get();

            string requestBody = ret;
            dynamic response = sg.client.mail.send.beta.post(requestBody: requestBody);

            
            model.Sent = true;
            return View(model);
        }


    }

}