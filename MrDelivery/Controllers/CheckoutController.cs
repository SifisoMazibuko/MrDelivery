using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;

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
                    //payment.Id = model.Id;
                    payment.Name = model.Name;
                    payment.cardNumber = model.cardNumber;
                    payment.month = model.month;
                    payment.year = model.year;
                    payment.securityCode = model.securityCode;
                    payment.Amount = model.Amount;
                };
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