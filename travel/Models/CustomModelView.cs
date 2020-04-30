using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class HomeModelView
    {
        public List<Post> Posts { get; set; }
        public List<Tour> Tours { get; set; }
        public List<TourDetail> TourDetails { get; set; }
    }
}