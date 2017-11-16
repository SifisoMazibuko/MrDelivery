using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int OrderName { get; set; }
        public DateTimeOffset dateTimeOffset { get; set; }
        public int Status { get; set; }
        public int Delivery { get; set; }
    }
}
