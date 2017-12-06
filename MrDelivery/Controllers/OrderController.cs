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
        public IActionResult Index(int id)
        {
            var orders = context.Carts.Where(od => od.Id == id);
            var viewModel = orders.Select(o => new OrderViewModel()
            {
                Id = o.Id,
                OrderName = o.ItemName,
                Status = "Pending...",
                dateTimeOffset = DateTimeOffset.Now
            }).ToList();
            //var order = new Order();

            //var viewModel = (from o in context.Carts
            //                 where o.Id == id
            //                 select o).ToList();
            //foreach (var item in viewModel)
            //{
            //    //order.Id = item.Id;
            //    //order.OrderName = item.ItemName;
            //    //order.Status = "pending";
            //    //order.Created = item.dateCreated;

            //    ViewBag.id = item.Id;
            //    ViewBag.orderName = item.OrderName;
            //    ViewBag.date = item.dateTimeOffset;
            //    ViewBag.status = item.Status;
            //}

            return View(viewModel);
        }
       

        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }
    }
}