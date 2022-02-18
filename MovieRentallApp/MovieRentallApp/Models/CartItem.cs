using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRentallApp.Models
{
    public class CartItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public double RunTime { get; set; }
        public double Price { get; set; }
        public double Rental { get; set; }
      
        
    }
}
