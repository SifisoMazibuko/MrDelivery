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
            var customer = (from c in context.Customers
                            select c ).ToList();
            return customer;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id, Customer customers)
        {
            Customer customer = null;
            if (_customers.TryGetValue(id, out customers))
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }
        
        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer == null){
                return BadRequest("Customer can not be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("", new { id = customer.Id, customer});
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
            return new StatusCodeResult(id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Customer customer = null;
            _customers.TryRemove(id, out customer);
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
