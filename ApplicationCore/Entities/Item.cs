using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Item : BaseEntity  
    {
        public int ItemName { get; set; }
        public int Description { get; set; }
        public decimal UnitPirce { get; set; }
    }
}
