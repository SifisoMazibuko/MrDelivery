﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MrDelivery.ViewModels;

namespace MrDelivery.Controllers
{
    public class RestaurantController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;

        public RestaurantController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }
        
        [HttpGet]
        public IActionResult Restaurants(RestaurantViewModel model)
        {
            var res = (from c in context.Restaurants
                       select c).ToList();
            ViewBag.res = res;
            return View(res);
        }

        [HttpGet]
        public IActionResult Menu(string search)
        {
            var resturantModel = (from r in context.Restaurants
                                  where r.Name == search
                                     join i in context.Items
                                     on r.Id equals i.Id
                                     select new CartItemViewModel
                                     {
                                        Id = i.Id,
                                        ItemName = i.ItemName,
                                        MenuType = i.MenuType,
                                        UnitPrice = i.UnitPrice,
                                        Description = i.Description,

                                        Name = r.Name,
                                        Location = r.Location,
                                        Icon = r.Icon,
                                        ImagePath = r.ImagePath,
                                        ItemType = r.ItemType,
                                        deliveryTime = r.deliveryTime,                                                                          

                                    });
            foreach(var item in resturantModel)
            {
                ViewBag.img = item.ImagePath;
                ViewBag.icon = item.Icon;
                ViewBag.name = item.Name;
                ViewBag.location = item.Location;
                ViewBag.itemType = item.ItemType;
            }

            //resturantModel = resturantModel.Where(p => p..StartsWith(searchProperty)
            //                                        || p.title.StartsWith(searchProperty)
            //                                        || p.location.Contains(searchProperty)
            //                                        || p.location.StartsWith(searchProperty)
            //                                        || p.Desc.StartsWith(searchProperty)
            //                                        || p.Desc.Contains(searchProperty)
            //                                        || p.category.Contains(searchProperty)
            //                                        || p.category.StartsWith(searchProperty)
            //                                        ); 
            return View(resturantModel);            
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }
    }
}