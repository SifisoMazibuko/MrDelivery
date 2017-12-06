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
            return View();
        }
        [HttpGet]
        public IActionResult Pay(int id)
        {
            ViewBag.amount = context.Items.Where(i => i.Id == id).GroupBy(x => x.Id).Select(g => g.First());
            return View();
        }
        [HttpPost]
        public IActionResult Pay(PaymentViewModel model)
        {
            System.Threading.Thread.Sleep(5000);
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
                context.Payments.Add(payment);
                context.SaveChanges();
                return RedirectToAction("SuccessPayment", "Checkout");
            }
            return View();
        }
        public IActionResult SuccessPayment()
        {
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