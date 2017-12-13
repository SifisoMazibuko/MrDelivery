﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrDelivery.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MrDelivery.Controllers
{
    public class HomeController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;

        public HomeController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        public IActionResult Index(string search)
      {
            if (!String.IsNullOrEmpty(search) )
            {
                var restaurant = context.Restaurants.Where(s => s.Location.StartsWith(search)
                               || s.Location.Contains(search)
                                );
                
                foreach(var a in restaurant)
                {
                    if (!string.IsNullOrEmpty(a.Location))
                    {
                        return RedirectToAction("Restaurants", "Restaurant");
                    }
                }
            }
            //if (ModelState.IsValid)
            //{
            //    if (!String.IsNullOrEmpty(search))
            //    {
            //        restaurant = restaurant.Where(s => s.Location.StartsWith(search)
            //                    || s.Location.Contains(search)
            //                    );
            //    }
            //    // return RedirectToAction("Restaurants", "Restaurant");
            //}

            ViewBag.res = context.Restaurants.ToList();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
