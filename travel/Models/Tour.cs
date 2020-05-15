using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class Tour
    {
        public long Id { get; set; } //Tour id đây là LONG
        public string Name { get; set; }
        public string Image { get; set; }
        public string Policy { get; set; }
        public string Schedule { get; set; }
        public DateTime DateStart { get; set; }
        public string Destination { get; set; }
        public string DeparturePlace { get; set; }
        public long CategoryTourId { get; set; } //Thấy long k
        public long Price { get; set; }  //Giá ở đây là giá tour
        public string ShortDes { get; set; } 
        public virtual CategoryTour CategoryTour { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CategoryTour
    {
        public long Id { get; set; } //Vì ở đây khai báo long nè
        public string Name { get; set; }
        [ForeignKey("CategoryTourId")]
        public virtual ICollection<Tour> Tours { get; set; }
    }
    public class TourModelView
    {
        public Tour tour { get; set; }
        public List<Tour> tours { get; set; }
    }
} 
//Xong, a vừa liên kết giữa user và tour, e cần update database tiếp

//Đó là lý do mà e nên tạo hết các model rồi mới tạo controller sau, để đảm bảo controller đầy đủ thông tin nhất có thể