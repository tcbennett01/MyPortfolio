using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace tbPortfolio.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "Id,Name,Email,Message,Phone")] Contact contact)
        {
            contact.Created = DateTime.Now;
            var newContact = contact.Name;

            var svc = new EmailService();
            var msg = new IdentityMessage();
            msg.Destination = "tcbennett01@gmail.com";
            msg.Subject = "Contact From Portfolio Site";
            msg.Body = "\r\n You have recieved a request to contact from " + newContact + " (" + contact.Email + ")" + "\r\n"
                         + "\r\n With the following message: \r\n\t"
                         + contact.Message;


            await svc.SendAsync(msg);

            return View(contact);
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult JSExercises()
        {
            return View();
        }
        public ActionResult Portfolio()
        {
            return View();
        }
    }
}