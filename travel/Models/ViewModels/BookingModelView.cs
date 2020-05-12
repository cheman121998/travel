using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class BookingModelView
    {
        public ApplicationUser user { get; set; }
        public Tour tour { get; set; }
    }
}