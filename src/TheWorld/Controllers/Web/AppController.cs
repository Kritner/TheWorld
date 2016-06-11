using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Business.Interfaces;
using TheWorld.Data;
using TheWorld.Repository;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _iMailService;
        private readonly IWorldRepository _repository;

        public AppController(IMailService iMailService, IWorldRepository repository)
        {
            _iMailService = iMailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository
                .GetAllTrips();

            return View(trips);
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
