using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ItemType { get; set; }
        public string ImagePath { get; set; }

        public static implicit operator Restaurants(string v)
        {
            throw new NotImplementedException();
        }
    }
}
