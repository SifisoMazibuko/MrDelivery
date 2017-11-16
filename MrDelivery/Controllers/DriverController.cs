using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MrDelivery.ViewModels.DriverAccount;

namespace MrDelivery.Controllers
{
    public class DriverController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;

        public DriverController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DriverView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DriverView(DriverViewModel model)
        {
            if (ModelState.IsValid) {
                Driver driver = new Driver
                {
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Location = model.Location,
                    Transport = model.Transport,
                    DriverLicence = model.DriverLicence,
                    Duration = model.Duration,
                    Availability = model.Availability
                };
                ViewData["success"] = "Successfully registered, We will Contact You Soon. Thank You!";


                var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();

                context.Drivers.Add(driver);
                context.SaveChanges();
                ModelState.Clear();
            }            
            
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }
    }
}