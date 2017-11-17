using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using ApplicationCore.Entities;
using MrDelivery.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace MrDelivery.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Restaurant")]
    public class RestaurantController : Controller
    {
        static HttpClient client = new HttpClient();
        private MrDeliveryContext context { get; set; }
        public RestaurantController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
        }
        // GET: api/Restaurant
        [HttpGet]
        public static async Task<Restaurants> Get(string data)
        {
            //var restaurant = (from r in context.Restaurants
            //                  select r).ToList();
            Restaurants res = null;
            HttpResponseMessage response = await client.GetAsync(data);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsStringAsync();
            }

            return res;
        }

        // GET: api/Restaurant/5
        //[HttpGet("{id}", Name = "Get")]
        //public IEnumerable<Restaurants> Get(int id)
        //{
        //    var restaurant = (from r in context.Restaurants
        //                      where r.Id == id
        //                      select r).ToList();
        //    return restaurant;
        //}

        // POST: api/Restaurant
        [HttpPost]
        public static async Task<Uri> Post([FromBody]Restaurants newRestaurant)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(newRestaurant), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Restaurant/", stringContent);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;

        }

        // PUT: api/Restaurant/5
        [HttpPut("{id}")]
        public static async Task<Restaurants> Put(int id, [FromBody]Restaurants updateRestaurant)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateRestaurant), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"api/Restaurant/{updateRestaurant.Id}", stringContent);
            // Deserialize the updated product from the response body.
            updateRestaurant = await response.Content.ReadAsStringAsync();
            return updateRestaurant;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public static async Task<HttpStatusCode> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Restaurant/{id}");
            return response.StatusCode;
        }

        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:49790/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var restaurant = new Restaurants { Name = "", Location = "", ItemType = "", ImagePath = "" };
                var url = await Post(restaurant);

                //Get 
                restaurant = await Get(url.PathAndQuery);
                Console.WriteLine($"RESTAURANT",  restaurant);
                //Update
                restaurant.ItemType = "Chicken wings, Burgers";
                await Put(restaurant.Id,restaurant);
                Console.WriteLine($"");
                //Delete
                var status = await Delete(restaurant.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)status})");
            }
            catch (Exception error)
            {
                throw new Exception("Error" + error.Message);
            }
        }
    }
}