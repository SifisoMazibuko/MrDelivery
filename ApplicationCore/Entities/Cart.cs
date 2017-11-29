using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset dateCreated { get; set; }
        public int ItemId { get; set; }
        //public virtual Item Item { get; set; }

        //public List<Item> _items = new List<Item>();
        //public IReadOnlyCollection<Item> items => _items.AsReadOnly();
        
    }
}
