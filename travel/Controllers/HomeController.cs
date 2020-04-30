using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using travel.Models;

namespace travel.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            List<Post> posts = db.Posts.ToList();

            //db là 1 context, 1 context có thể là 1 object kết nối đến các nơi chứa dữ liệu (cache, redis, database)...
            //Vì bản chất hắn k trực tiếp kết nối đến CSDL, hắn chỉ là 1 driver trung gian, mình khai báo cái gì nó mới làm việc đấy
            var tours = db.Tours.ToList();
           
            var toursLimit = db.Tours.Where(tour => tour.Id > 10).Count();
            //Trả về số lượng tour có id > 10
            //Hiểu câu lệnh ni là chi k? Đếm số lượng tour giới hạn có id >10

            var postsCategory = db.Posts.Where(post => post.Category.Name == "Food").Take(10).ToList();
            //Man :(( Cái ni làm gì? Vào trong bài post có Name = "FOOD" lấy ra danh sách 10 bài post đàu tiên
            //Anh :)) => Trả về danh sách 10 thằng post đầu tiên có (category name = food) thể loại là Food

            Post newestPost = db.Posts.Where(post => post.Content != "").OrderByDescending(post => post.UpdatedAt).First();
            //Sắp xếp các bài viết theo UpdateAt tăng dần rồi sau đó trả ra bài đăng đầu tiên cso Content khác rỗng = "Có nội dung?"
            // Có 2 lý do mà a nói e chậm là vì ri, không chịu để ý chi hết. newestPost là gì? bài viết mới nhất, Descending = giảm dần, nếu k hiểu 1 trong 2 cái thì dựa vào 1 trong 2 cái là có thể suy luận đc ý nghĩa
            // Phân tách câu để hiểu rõ db.Posts.Where(post => post.Content != "") => danh sách posts có nội dung 
            // .OrderByDescending(post => post.UpdatedAt) => Sắp xếp giảm dần theo ngày cập nhật = mới nhất
            // .First() lấy ra thằng đầu tiên
            // Trả về post đầu tiên trong danh sách Posts có nội dung sắp xếp giảm dần theo ngày cập nhật = Trả về post mới nhất

            //Okie chưa? Dạ rồi. Biết nó là gì rồi, làm gì cũng biết rồi, hiểu và biết cách sử dụng chưa, còn thắc mắc mô k, hỏi luôn

            // 2 model in a single view :D cái này e làm đc rồi sao lại hỏi anh? đừng show a kết quả, kết quả là thứ mà e áp dụng k được, hoặc áp dụng sai, hoặc lỗi c ú pháp
            // logic cơ bản e có hết rồi, công thức cũng có, Model cũng có, cách load model cũng có, 2 model hay 1 model cũng như vậy, 1 Model chứa nhiều class thì hắn cũng như multi model rồi
            // Cái này trả về 1 danh sách dạng dict, cái ni trả về 1 tổng, cái ni đếm số lượng
            //var tours = db.Tours.ToList();
            //Thấy kiểu dữ liệu thằng ni chưa, T là chi? Tour, hiểu dòng code ni để làm chi k?
            //Khai báo 1 list các tour được gán cho thằng sau, Ở sau là lấy danh sách các tour trong service
            //Lấy danh sách posts, xem thấy kiểu dữ liệu là chi k? List, T là gì, đọc dòng cuối a cái
            //đại diện cho những loại danh sách của đối tươ... Dòng cuối T is Post :D thấy rõ chưa? dạ rồi, ở đây var hay khai báo như vậy vẫn đc, vì var tự định nghĩa kiểu dữ liệu đối với hàm trả về đúng kiểu

            var model = new HomeModelView(); //Vì kiểu dữ liệu có thể xác định nên chỉ cần dùng var là đc
            model.Posts = db.Posts.ToList(); //Lấy mấy bài viết ra? điều kiện nào? cần sắp xếp không?
            model.Tours = db.Tours.ToList();
            //... Thiếu chi thì tự bổ sung vào (chơ k lại bảo anh k code...)
            return View(model); // Truyển model sang view kiểu gì?
            //return View(await db.Posts.ToListAsync());
        }

        //public actionresult index()
        //{
        //    return view();
        //}

       public ActionResult About() { 
            ViewBag.Message = "Đây là dòng code about thay đổi ở controller. Phải build nó mới thay đổi";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}