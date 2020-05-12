using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class TravelService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TaxCode { get; set; }
        public string Represent { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public long Nnkd { get; set; } // Ngành nghề kinh doanh //Cái chi ri thuộc tính của travel service thôi anh, ý anh là gì, em search thuộc tính của travel service mà nó ra từ viết tắt vậy luôn chứ em k biết :D mà em thays 1 dãy số, e k biết nó là gì, cũng k biết nó dùng để làm giề trong hệ thống mà cũng đưa vào đc
        public string UserId { get; set; } 
        public virtual ApplicationUser User { get; set; }
    }
}

//Giờ bị gì á, 
///Hôm qua anh connect Tour với User, nhưng theo Database của em là connect TourDetail với User nên em sửa lại, ai ngờ ra lỗi
///Xog em fix, em đổi rồi ra lỗi khác, nhiều quá xong em định xóa đi làm lại,,,mà không được :(((((((((((((
/// ra lỗi gì? lỗi Tour.cs TourController...có gạch đỏ ở dưới á anh, rứa răng k tìm cách fĩ lỗi \// Thì em tìm rồi đó, mà fix không hết lỗi
/// mà ngày càng ra nhiều, rứa thì có phải e fix đâu :v rồi giờ bị gì để a check, chứ a thấy có lỗi gì đâu, errổ list trống này
/// Tour controller đâu hết cả rồi, em định xóa đi làm lại,,,nhưng khi làm lại thì hiển thị cửa sổ lỗi khác nuẫ, em xoá kiểu chi rứa
/// add user vô mà k vô usẻ add à, chưa chạy database update luôn, mà đã tạo controller rồi, hèn chi k báo lỗi
/// database-update
/// Em có chạy dbupdate nhưng nó ra lỗi nên em next em chạy qua tạo controller luoonaf
/// Rứa hắn ra lỗi như ri hay lỗi khác khác anh, nói chung là 1 dạng của fail, chứ k ra như này