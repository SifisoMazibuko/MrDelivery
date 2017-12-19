using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;

namespace MrDelivery.Controllers
{
    public class OrderController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;

        public OrderController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        [HttpGet]
        public IActionResult Index(int userId)
        {
            userId = Convert.ToInt32(TempData["customerId"]);
            var odr = new OrderViewModel();
            var orderz = context.Order.Where(od => od.Id == userId);
            foreach (var item in orderz)
            {
                odr.Delivery = item.Delivery;
            }
            if (userId > 0) {
                var time = Convert.ToInt32(odr.Delivery);
                if (time <= 46)
                {
                    odr.Status = "pending...";
                }
                else {
                    odr.Status = "Delivered";
                }
                var orders = context.Order.Where(od => od.Id == userId);
                var viewModel = orders.Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    OrderName = o.OrderName,
                    Status = odr.Status,
                    dateTimeOffset = DateTime.UtcNow.ToLocalTime(),
                    Delivery = DateTime.Today.AddMinutes(45).ToString("mm")
                 }).ToList();
                
               
                return View(viewModel);                
            }
            else {
                return RedirectToAction("Login", "Account");
            }                    
        }      

        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }
    }
}