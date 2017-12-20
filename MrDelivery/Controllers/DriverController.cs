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
using MimeKit;

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

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
                message.To.Add(new MailboxAddress(model.Email));
                message.Subject = "Welcome to MrDeliverFood";

                message.Body = new TextPart
                {
                    Text = string.Format("Hi " + model.FullName + ", \n" +
                            "\nWelcome to MrDelivery Food Service!!!" +
                            "\n\n"
                            + "We have received your application to become our driver.\n"
                            + "Be on the look out for an email, we will contact you soon.\n"
                            + "\n\nYour Information as follows:\n"
                            + "\nFullName:\t" + model.FullName
                            + "\nPhoneNumber:\t" + model.PhoneNumber
                            + "\nEmail:\t" + model.Email
                            + "\nLocation:\t" + model.Location
                            + "\nDriverLicence:\t" + model.DriverLicence
                            + "\nDuration:\t" + model.Duration
                            + "\nAvailability:\t" + model.Availability
                            + "\n\n\n" + "Thank you!"
                            + "\n\n" + "Kind Regards,"
                            + "\n Team MrDFood\n"
                            + "+2761 010 1256 \n"
                            + "MrDfood@MrDfood.co.za"
                    )


                };


                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate("mazibujo19@gmail.com", "Secretive2017");

                    client.Send(message);
                    client.Disconnect(true);
                }
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