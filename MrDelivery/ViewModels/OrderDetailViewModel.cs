using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class OrderDetailViewModel
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public String ItemName { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Total { get; set; }
    }
}
