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
using MrDelivery.ViewModels;

namespace MrDelivery.Controllers
{
    //[Route("[Controller]/[Action]")]
    //[Authorize]
    public class AccountController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private MrDeliveryContext context { get; set; }
        public ApplicationCore.Interfaces.Provider.IUserCustomer CustomerProvider { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (ModelState.IsValid)
                {
                    if (isValid(model.Email, model.Password))
                    {
                        //var customer = (from c in context.Customers
                        //                where c.email == model.Email && c.password == model.Password
                        //                select c).ToList();
                      var customer = context.Customers.Where(c => c.email == model.Email && c.password == model.Password).ToList();
                        
                       foreach (var cus in customer)
                        {
                            ViewData["firstName"] = cus.firstName;
                            TempData["customerId"] = cus.Id;
                            TempData["Firstname"] = cus.firstName;
                        }
                    }
                    ViewData["ReturnUrl"] = returnUrl;
                    var errors = ModelState
                           .Where(x => x.Value.Errors.Count > 0)
                           .Select(x => new { x.Key, x.Value.Errors })
                            .ToArray();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (LoginException err)
            {
                throw new Exception("Error: Please try again" + err.Message.ToString());
            }

            //var results = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            //if (results.Succeeded)
            //{
            //    return RedirectToAction(returnUrl);
            //}
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
            string name = "";
            var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();
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

                    foreach (var cus in context.Customers)
                    {
                        name = cus.firstName;
                    }
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
                    message.To.Add(new MailboxAddress(model.email));
                    message.Subject = "Welcome to MrDeliverFood";
                    //message.Body = new TextPart
                    //{
                    //    Text = string.Format("Hi " + name + ", welcome to MrDelivery Food Service, Hope you enjoy our service"),

                    //};

                    message.Body = new TextPart
                    {
                        Text = string.Format("Hi " + name + "," +
                                "\nWelcome to MrDelivery Food Service." +
                                "\n\n"
                                + "Hope you enjoy our service."
                                + "\n\n Your Username: " + model.email
                                + "\n\n Password: " + model.password
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
                    
                    context.Customers.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (RegisterException err)
            {
                throw new Exception("Error: Please try again" + err.Message.ToString());
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
        [HttpGet]
        public IActionResult MyAccount(int? userId)
        {
            userId = Convert.ToInt32(TempData["customerId"]);
            var cus = new IndexViewModel();
            var customermodel = context.Customers.Where(c => c.Id == userId).ToList();
            foreach (var item in customermodel)
            {
                cus.Username = item.firstName;
                cus.email = item.email;
                cus.phoneNumber = item.phoneNumber;
            }
            return View(cus);
        }
        [HttpPost]
        public IActionResult MyAccount(IndexViewModel model, int userId)
        {
            userId = Convert.ToInt32(305936821);
            if (ModelState.IsValid)
            {
                var user = new Customer();
                {
                    user = context.Customers.Find(userId);
                    user.email = model.email;
                    user.firstName = model.Username;
                    user.phoneNumber = model.phoneNumber;
                };
                
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                model.email = string.Empty;
                model.Username = string.Empty;
                model.phoneNumber = string.Empty;
                TempData["success"] = "Successfully updated!";
                return View(model);
            }
            return View();
        }
       

        [HttpGet]
        public IActionResult ChangePassword(int? userId)
        {
            userId = Convert.ToInt32(TempData["customerId"]);
            var cus = new ChangePasswordViewModel();
            var customermodel = context.Customers.Where(c => c.Id == userId).ToList();
            foreach (var item in customermodel)
            {
                cus.password = item.password;
                cus.confirmPassword = item.confirmPassword;
            }
            return View(cus);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model, int userId)
        {
            userId = Convert.ToInt32(305936821);
            if (ModelState.IsValid)
            {
                var user = new Customer();
                {
                    user = context.Customers.Find(userId);
                    user.password = model.password;
                    user.confirmPassword = model.confirmPassword;
                };

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                model.password = string.Empty;
                model.confirmPassword = string.Empty;
                TempData["success"] = "Your password has been changed.";
                return View(model);
            }
            return View();
           
        }

        public IActionResult SendVerificationEmail(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Logout()
        {
            TempData["Firstname"] = null;

            return RedirectToAction("Index","Home");
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