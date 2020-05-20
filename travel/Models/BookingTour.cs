using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class BookingTour
    {
        public long Id { get; set; }
        public int CountAdult { get; set; }
        public int CountChild { get; set; }
        public double Price { get; set; } //Giá ở đây là tổng số tiền mà khách phải trả
        public DateTime DateBook { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public long TourId { get; set; } //Răng ở đây lại là string? Vì em quên lài UserId MVC của họ mặc định là string, chứ k phải là để vào đó là string,,, oki a
        public virtual Tour Tour { get; set; }

    }
}