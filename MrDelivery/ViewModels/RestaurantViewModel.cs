using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.ViewModels
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ItemType { get; set; }
        public string Imagepath { get; set; }
    }
}
