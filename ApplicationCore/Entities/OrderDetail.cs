using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public String ItemName { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Total { get; set; }
    }
}
