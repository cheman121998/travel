using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class HomeModelView
    {
        public List<Post> Places { get; set; }
        public List<Post> Foods { get; set; }
        public List<Tour> Tours { get; set; }
        public List<BookingTour> TourDetails { get; set; }
    }
}