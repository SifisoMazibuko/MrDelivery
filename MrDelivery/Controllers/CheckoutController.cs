using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

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

        public IActionResult Checkout()
        {
            System.Threading.Thread.Sleep(100000);
            return View();
        }

        public IActionResult Pay()
        {
            return View();
        }
    }
}