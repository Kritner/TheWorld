using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Business.Interfaces;
using TheWorld.Data;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _iMailService;
        private readonly WorldContext _worldContext;

        public AppController(IMailService iMailService, WorldContext worldContext)
        {
            _iMailService = iMailService;
            _worldContext = worldContext;
        }

        public IActionResult Index()
        {
            var trips = _worldContext
                .Trips
                .OrderBy(ob => ob.Name)
                .ToList();

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
