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

    }
}
