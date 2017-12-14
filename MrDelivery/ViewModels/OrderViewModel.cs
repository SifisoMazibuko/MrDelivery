using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }
        public string OrderName { get; set; }
        public DateTimeOffset dateTimeOffset { get; set; }
        public string Status { get; set; }
        public String Delivery { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
