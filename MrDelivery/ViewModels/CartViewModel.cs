using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset dateCreated { get; set; }
    }
}
