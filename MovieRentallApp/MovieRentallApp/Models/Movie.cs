using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRentallApp.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genres { get; set; }
        public string Cast { get; set; }
        public string ImgUrl { get; set; }
        public string ReleaseDate { get; set; }
        public string Description { get; set; }
        public double RunTime { get; set; }
        public double Price { get; set; }
        public double Rental { get; set; }
    }
}
