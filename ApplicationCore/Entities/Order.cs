using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Order : BaseEntity
    {
        public String OrderName{ get; set; }
        public DateTimeOffset Created { get; set; }
        public String Status{ get; set; }
        public DateTimeOffset Delivery { get; set; }
        //public virtual Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
