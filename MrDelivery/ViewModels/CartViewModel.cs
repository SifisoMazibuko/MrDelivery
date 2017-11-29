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
        public string ItemName { get; set; }
        public string MenuType { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTimeOffset dateCreated { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }
    }
}
