using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Item : BaseEntity  
    {
        public string ItemName { get; set; }
        public string MenuType { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        //public int Quantity { get; set; }
        public List<Cart> _items = new List<Cart>();
        public IReadOnlyCollection<Cart> items => _items.AsReadOnly();
    }
}
