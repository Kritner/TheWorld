using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Business.Interfaces;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _iMailService;

        public AppController(IMailService iMailService)
        {
            _iMailService = iMailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            string email = Startup.Configuration["AppSettings:SiteEmailAddress"];

            if (string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Could not send email, configuration problems.");
            }

            if (ModelState.IsValid)
            {
                
                if (_iMailService.SendMail(
                    email,
                    email,
                    $"Contact page from {model.Name} ({model.Email})",
                    model.Message
                ))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }
    }
}
