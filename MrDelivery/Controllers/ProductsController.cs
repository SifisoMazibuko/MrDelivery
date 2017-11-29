﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MrDelivery.ViewModels;
using ApplicationCore.Entities;

namespace MrDelivery.Controllers
{
    public class ProductsController : Controller
    {
        private MrDeliveryContext context { get; set; }
        private bool _disposeContext = false;
       // public string cartId { get; set; }
       // public const string cartSession = "CartId";

        public ProductsController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
            _disposeContext = true;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            
            ViewBag.itemsAdded = context.Items.Where(i => i.Id == id).GroupBy(x=>x.Id).Select(g => g.First());
            ViewBag.itemRes = context.Restaurants.Where(i => i.Id == id);
            //ViewBag.itemsAdded = context.Items.GroupBy(x => x.Id);
            CartViewModel model = new CartViewModel();

            //foreach(var item in itemsAdded)
            //{
            //    model.Id = item.Id;
            //    model.ItemName = item.ItemName;
            //    model.Description = item.Description;
            //    model.UnitPrice = item.UnitPrice;
            //    model.MenuType = item.MenuType;
            //}
            
            return View(model);
        }
        [HttpPost]
        public IActionResult AddToCart(CartViewModel model)
        {
            if (model?.Id == null)
            {
                return RedirectToAction("Index","Home");
            }
            //var item = new Cart();
            //item.Id = model.Id;
            //item.Quantity = model.Quantity;
            //item.dateCreated = model.dateCreated;
            var errors = ModelState
                          .Where(x => x.Value.Errors.Count > 0)
                          .Select(x => new { x.Key, x.Value.Errors })
                           .ToArray();
            var itemInCart = new Item();
            {
                itemInCart.Id = model.Id;
                itemInCart.ItemName = model.ItemName;
                itemInCart.Description = model.Description;
                itemInCart.MenuType = model.MenuType;
                itemInCart.UnitPrice = model.UnitPrice;
            };
            
            context.Items.Add(itemInCart);
            context.SaveChanges();

            return View(model);
        }

        public void RemoveFromCart(string removeCartID, int removeItemID)
        {
            using (context = new MrDeliveryContext())
            {
                try
                {
                    var myItem = (from c in context.Carts
                                  where c.Id == removeItemID && c.ItemId == removeItemID
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        context.Carts.Remove(myItem);
                        context.SaveChanges();
                    }

                }
                catch (Exception err)
                {
                    throw new Exception("Error: Unable to remove from cart " + err.Message.ToString());
                }
            }
        }
        public void UpdateCart(int updateCartID, int updateItemCart, int quantity)
        {
            using (context = new MrDeliveryContext())
            {
                try
                {
                    var myItem = (from c in context.Carts
                                  where c.Id == updateCartID && c.ItemId == updateItemCart
                                  select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.Quantity = quantity;
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: Unable to update the cart " + ex.Message.ToString());
                }
            }
        }
        public string GetCartId()
        {
            if (HttpContext.Session.GetString("cartSession") == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                {
                }
                else {
                }
            }
            return HttpContext.Session.GetString("cartSession").ToString();
        }
        protected override void Dispose(bool disposing)
        {
            if (_disposeContext)
                context.Dispose();

            base.Dispose(disposing);

        }
    }
}