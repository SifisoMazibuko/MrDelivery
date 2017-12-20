using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;
using MimeKit;
using MrDelivery.ViewModels.Account;

namespace MrDelivery.Controllers
{
    public class CheckoutController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;

        public CheckoutController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Checkout(int id)
        {
            ViewBag.checkout = context.Items.Where(i => i.Id == id).GroupBy(x => x.Id).Select(g => g.First());
            return View();
        }
        [HttpPost]
        public IActionResult Checkout()
        {
            System.Threading.Thread.Sleep(59000);
            int userId = Convert.ToInt32(TempData["customerId"]);

            if (userId > 0)
            {
                return View();
            }
            else {
                    return RedirectToAction("Login", "Account");
            }

        }
        [HttpGet]
        public IActionResult Pay(int id)
        {
            decimal service = 20;
            var amount = context.Items.Where(i => i.Id == id).GroupBy(x => x.Id).Select(g => g.First());
            foreach (var item in amount)
            {
                ViewBag.uprice = item.UnitPrice + service;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Pay(PaymentViewModel model)
        {
            System.Threading.Thread.Sleep(5000);

            var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();

            if (ModelState.IsValid)
            {  
                var payment = new Payment();
                {
                    payment.Name = model.Name;
                    payment.cardNumber = model.cardNumber;
                    payment.month = model.month;
                    payment.year = model.year;
                    payment.securityCode = model.securityCode;
                    payment.Amount = model.Amount;
                };
                
                var cus = new RegisterViewModel();
                int userId = Convert.ToInt32(TempData["customerId"]);
                var customer = context.Customers.Where(c => c.Id == userId);
                foreach (var item in customer)
                {
                    cus.email = item.email;
                }

                var order = context.Order.Find(userId);
                var mod = new OrderViewModel();
                var myItems = context.Order.Where(i => i.Id == userId).OrderByDescending(x => x.Id).Take(1).ToList();
              
                foreach (var item in myItems)
                {
                    mod.Description = item.Description;
                    mod.UnitPrice = item.UnitPrice;
                }
                var myItemz = context.Order.Where(i => i.Id == userId).OrderByDescending(x => x.Id).Last();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sifiso Mazibuko", "mazibujo19@gmail.com"));
                message.To.Add(new MailboxAddress(cus.email));
                message.Subject = "ORDER RECEIVED: " +order.Id;
                message.Body = new TextPart
                {
                    Text = string.Format("Hi " + payment.Name + "," +
                                "\n\nYour order was received and is been processed. " +
                                "\n\n"
                                +"Order Number: "+ order.Id+ ""
                                +"\nOrder Name: "+ order.OrderName + ""
                                +"\nOrder Description: " + myItemz.Description + ""
                                +"\nOrder Price: R " + myItemz.UnitPrice + ""
                                +"\nOrder Date: "+ order.Created + ""
                                +"\nOrder Delivery: "+ order.Delivery + " min"
                                +"\n\n\n"+ "Thank you!" 
                                +"\n\n"+ "Kind Regards,"
                                +"\n Team MrDFood\n"
                                +"+2761 010 1256 \n"
                                +"MrDfood@MrDfood.co.za"
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

                context.Payments.Add(payment);
                context.SaveChanges();

               
                return RedirectToAction("SuccessPayment", "Checkout");
            }

            return View(model);
        }

        public IActionResult SaveAddress(int id)
        {
            ViewBag.checkout = context.Items.Where(i => i.Id == id).GroupBy(x => x.Id).Select(g => g.First());
            return View();
        }
        [HttpPost]
        public IActionResult SaveAddress(AddressViewModel model, int id)
        {
            var errors = ModelState
                      .Where(x => x.Value.Errors.Count > 0)
                      .Select(x => new { x.Key, x.Value.Errors })
                       .ToArray();

            if (ModelState.IsValid)
            {
                var address = new Address();
                {
                    //address.Id = model.Id;
                    address.StreetNo = model.StreetNo;
                    address.UnitNo = model.UnitNo;
                    address.BuildingType = model.BuildingType;
                    address.ComplexName = model.ComplexName;
                };
                context.Addresses.Add(address);
                context.SaveChanges();
                //TempData["Address"] = "Address Successfully saved";
            }
           
            return RedirectToAction("SaveAddress", new { id = id});
           
        }

        public IActionResult SuccessPayment()
        {
            System.Threading.Thread.Sleep(5000);
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