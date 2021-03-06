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
            TempData["search"] = search;
            var res = (from c in context.Restaurants
                       where c.Location == search || c.SearchLocation == search
                       select c).ToList();
            ViewBag.res = res;
            if (res.Count == 0) {

                return View();
                            //eturn RedirectToAction("Restaurants", "Restaurant");                        
            }
            else
            {
                if (!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
                {
                    var restaurant = context.Restaurants.Where(s => s.SearchLocation.StartsWith(search)
                                    || s.SearchLocation.Contains(search)
                                    ).Where(c => c.Location == search || c.SearchLocation == search);

                    foreach (var a in restaurant)
                    {
                        if (!string.IsNullOrEmpty(a.Location))
                        {
                            //return View(res);
                            return RedirectToAction("Restaurants", "Restaurant");
                        }
                    }
                }
                return View(res);
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

            //iewBag.res = context.Restaurants.ToList();
            //return View();
        }


        //public IActionResult GetRestaurants(string search)
        //{
        //    var res = (from c in context.Restaurants
        //               where c.Location == search || c.SearchLocation == search
        //               select c).ToList();
        //    ViewBag.res = res;
        //    if (!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
        //    {
        //        var restaurant = context.Restaurants.Where(s => s.SearchLocation.StartsWith(search)
        //                        || s.SearchLocation.Contains(search)
        //                        ).Where(c => c.Location == search || c.SearchLocation == search);

        //        foreach (var a in restaurant)
        //        {
        //            if (!string.IsNullOrEmpty(a.Location))
        //            {


        //                return View(res);
        //                //eturn RedirectToAction("Restaurants", "Restaurant");
        //            }
        //        }
        //    }
        //    return View(res);
        //}
        //public IQueryable<Restaurant> GetRestaurants(RestaurantViewModel search)
        //{
        //    var restaurant = context.Restaurants.AsQueryable();
        //    if (search != null)
        //    {
        //        if (search.Id != 0)
        //            restaurant = restaurant.Where(x => x.Id == search.Id);
        //        if (!string.IsNullOrEmpty(search.Name))
        //            restaurant = restaurant.Where(x => x.Name.Contains(search.Name));
        //        if (!string.IsNullOrEmpty(search.Location))
        //            restaurant = restaurant.Where(x => x.Location.Contains(search.Location));
        //        if (search.Location != null)
        //            restaurant = restaurant.Where(x => x.Location == search.Location);

        //    }
        //    return results;
        //}
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
