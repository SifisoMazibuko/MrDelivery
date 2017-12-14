using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

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
        [HttpPost]
        public IActionResult Menu(CartItemViewModel model, int userId)
        {
            userId = Convert.ToInt32(TempData["customerId"]);
            var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { x.Key, x.Value.Errors })
                         .ToArray();
            if (userId > 0)
            {
                if (ModelState.IsValid)
                {
                    var itemInCart = new Cart();
                    {
                        itemInCart.Id = model.Id;
                        itemInCart.ItemName = model.ItemName;
                        itemInCart.Description = model.Description;
                        itemInCart.MenuType = model.MenuType;
                        itemInCart.UnitPrice = model.UnitPrice;
                        itemInCart.CartId = (new Random()).Next(1, 5);
                        itemInCart.dateCreated = DateTimeOffset.Now;
                        //itemInCart.Quantity = model.Quantity;
                    };
                    var order = new Order();
                    {
                        order.Id = userId;
                        order.OrderName = model.ItemName;
                        order.Created = DateTimeOffset.Now;
                        order.Status = "pending";
                        order.Description = model.Description;
                        order.UnitPrice = model.UnitPrice;
                        order.Delivery = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy");
                    };

                    context.Order.Add(order);
                    context.Carts.Add(itemInCart);
                    context.SaveChanges();
                    return RedirectToAction("AddToCart", "Products", new { id = model.Id });
                }
            }
            else {
                return RedirectToAction("Login", "Account");
            }
               

            ViewBag.menu = model;
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