using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MrDelivery.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Provider;
using ApplicationCore.Exceptions;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace MrDelivery.Controllers
{
    //[Route("[Controller]/[Action]")]
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private MrDeliveryContext context { get; set; }
        public ApplicationCore.Interfaces.Provider.IUserCustomer CustomerProvider { get; set; }
        private bool _disposeContext = false;

        public AccountController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model,string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if(isValid(model.Email, model.Password))
                {
                    var customer = (from c in context.Customers
                                    where c.email == model.Email && c.password == model.Password
                                    select c).ToList();

                    foreach (var cus in customer)
                    {
                        ViewData["cusId"] = cus.Id;
                        ViewData["firstname"] = cus.firstName;
                        ViewData["LastName"] = cus.lastName;
                    }
                }
                ViewData["ReturnUrl"] = returnUrl;
                var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();
                return RedirectToAction("Index","Home");
            }
            //ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new Customer();
                    {
                        user.Id = model.Id;
                        user.firstName = model.firstName;
                        user.lastName = model.lastName;
                        user.email = model.email;
                        user.password = model.password;
                        user.confirmPassword = model.confirmPassword;
                        user.phoneNumber = model.phoneNumber;
                    };

                    //var u = CustomerProvider.saveCustomer (model.Id, model.firstName, model.lastName, model.email,
                   // model.password,model.confirmPassword,model.phoneNumber); 
                    var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();
                    context.Customers.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (RegisterException e)
            {
                string msg = e.Message;
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(RegisterViewModel model)
        {
            string tempPassword = "";
            string name = "";
            string pass = "";
            var getPassword = (from psd in context.Customers
                                   where psd.email == model.email
                                   select psd).ToList();
            foreach (var item in getPassword)
            {
                tempPassword = item.password;
                name = item.firstName;
                pass = item.password;
            }
            ViewData["passwordsuccess"] = "Successfully sent, Please check your email inbox!";

            var message = new MimeMessage();
            message.From.Add (new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
            message.To.Add(new MailboxAddress(model.email));
            message.Subject = "Password";

            var body = string.Format("Hi " + name + " Your password is: " + pass + " Thank You.");

            //message.Body = string.Format("Hi " + name + ",<br /><br />Your password is: " + pass + "<br /><br />Thank You. <br /> Regards, <br /> MrDelivery  <img src=cid:mrd-food.jpg/>");
            message.Body = new TextPart
            {
                Text = string.Format("Hi " + name + ", Your password is: " + tempPassword + " Thank You."),
                
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient()) {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.gmail.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("mazibujo19@gmail.com", "Secretive2017");

                client.Send(message);
                client.Disconnect(true);
            }

                model.email = string.Empty;
            ModelState.Clear();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                   context.Dispose();

            base.Dispose(disposing);

        }

        public bool isValid(string email, string password)
        {
            var customer = (from c in context.Customers
                            where c.email == email && c.password == password
                            select c).ToList();
            if (customer.Count > 0)
                return true;
            else
                return false;
        }
        
    }
}