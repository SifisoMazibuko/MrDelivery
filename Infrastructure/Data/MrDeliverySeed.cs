using ApplicationCore.Entities;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public static class MrDeliverySeed 
    {
        public static void Initialize(MrDeliveryContext context)
        {
            if (context.Restaurants.Any())
            {
                return;
            }
            var resturants = new Restaurants[]
            {
                 new Restaurants() {
                        Name = "McDonald",
                        Location = "Bryanston",
                        ItemType = "Chicken, Chicken Wings, Burgers, Breakfast, Salad",
                        ImagePath = "http://catalogbaseurltobereplaced/images/products/1.png"
                },
                new Restaurants() {
                        Name = "Spurs",
                        Location = "Bryanston",
                        ItemType = "American, Chicken Wings, Burgers, Seafood",
                        ImagePath = "http://catalogbaseurltobereplaced/images/products/1.png"
                }
            };
            context.SaveChanges();
        }

        //public static async Task SeedAsync(MrDeliveryContext context)
        //{
        //    if (context.Restaurants.Any()) {
        //        context.Restaurants.AddRange(GetRestaurant());
        //        return;
        //    }
        //    await context.SaveChangesAsync();
        //    try
        //    {
        //        if (context.Restaurants.Any())
        //        {
        //            context.Restaurants.AddRange(GetRestaurant());
        //            await context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //    }
        //}
        //static IEnumerable<Restaurants> GetRestaurant()
        //{
        //    return new List<Restaurants>()
        //    {
        //        new Restaurants() {
        //                Name = "McDonald",
        //                Location = "Bryanston",
        //                ItemType = "Chicken, Chicken Wings, Burgers, Breakfast, Salad",
        //                ImagePath = "http://catalogbaseurltobereplaced/images/products/1.png"
        //        },
        //        new Restaurants() {
        //                Name = "Spurs",
        //                Location = "Bryanston",
        //                ItemType = "American, Chicken Wings, Burgers, Seafood",
        //                ImagePath = "http://catalogbaseurltobereplaced/images/products/1.png"
        //        }

        //    };
        //}
    }
}
