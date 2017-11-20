using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using System.Collections.Concurrent;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace MrDelivery.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private MrDeliveryContext context { get; set; }
        static ConcurrentDictionary<string, Customer> _customers = new ConcurrentDictionary<string, Customer>();
        public CustomerController(DbContextOptions<MrDeliveryContext> option)
        {
            context = new MrDeliveryContext(option);
        }
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var customer = context.Customers.ToList();
            return customer;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<Customer> Get(int id, Customer customers)
        {
            var customer = context.Customers.Where(c => c.Id == id).ToList();
            return customer;
        }
        
        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
           /* using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                customer = new Customer
                {
                    firstName = "Marike",
                    lastName = "fourie",
                    password = "01234567",
                    confirmPassword = "01234567",
                    phoneNumber = "0745896521",
                    email = "marike@reverside.co.za"
                };
                client.BaseAddress = new Uri("http://localhost:49790/");
                var response = client.PostAsync("api/Customer/", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("error");
                }
            }*/

            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            context.Customers.Add(new Customer
            {
                Id = customer.Id,
                firstName = customer.firstName,
                lastName = customer.lastName,
                email = customer.email,
                password = customer.password,
                confirmPassword = customer.confirmPassword,
                phoneNumber = customer.phoneNumber,
            });
            context.SaveChanges();
            return Ok();
        }
        
        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer customer)
        {
            if (customer == null){ 
                return BadRequest("Customer can not be null");
            }
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if (customer.Id != id){
                return BadRequest($"product.id dont match the id parameter");
            }
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                customer = new Customer
                {
                    firstName = "Marike",
                    lastName = "fourie",
                    password = "01234567",
                    confirmPassword = "01234567",
                    phoneNumber = "0745896521",
                    email = "marike@reverside.co.za"
                };
                client.BaseAddress = new Uri("http://localhost:49790/");
                var response = client.PutAsync("api/Customer/", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("error");
                }
            }
            return new StatusCodeResult(id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
