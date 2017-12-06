using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Cart:BaseEntity
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string MenuType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset dateCreated { get; set; }
        //public int ItemNo { get; set; }
        //public virtual Item Item { get; set; }

        //public List<Item> _items = new List<Item>();
        //public IReadOnlyCollection<Item> items => _items.AsReadOnly();
        
    }
}
