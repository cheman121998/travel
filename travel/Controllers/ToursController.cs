using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using travel.Models;
using Microsoft.AspNet.Identity;

namespace travel.Controllers
{
    public class ToursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tours
        public async Task<ActionResult> Index()
        {
            var tours = db.Tours.Include(t => t.CategoryTour);
            return View(await tours.ToListAsync());
        }

        //Get: TravelTour
        //như này là bắt login vào mới đc dùng => okie cái đăng nhập này chưa, controller nào cần thì cứ nhét cái đó zô trên cái controller là đc dạ, do em k hiểu chữ nào,, tưởng anh đang hỏi
        public async Task<ActionResult> TravelTour(string search="", int page=1)
        {
            ViewBag.page = page;
           
                var tours = db.Tours.Where(x => x.Name.Contains(search) || x.DeparturePlace.Contains(search)).OrderByDescending(x => x.CreatedAt).Skip((page - 1) * 12).Take(6).ToList();
                return View(tours);
           
        }
        //Giờ BE cần gì nào, cái trang đặt tour,,, cần lưu thông tin user và tour =, lấy được thông tin tour
        // Chức năng gì trước, cái nào cần trước? Danh sách tour hay là chi tiết tour hay là đặt tour????  lấy thông tin tour cần trước, xong lưu thông tin
        // Dạ
        // Lấy danh sách chi tiết tour   danh sách chi tiết để show chỗ nào?  trang này là ĐẶT TOUR, thì e kêu là chức năng ĐẶT TOUR, cần thông tin  ABC, chơ chức năng nào k nói rõ sao a biết
        // Làm phân quyền chưa?
        /// <summary>
        //Chwua luôn anh
        /// </summary>
        /// <returns></returns>
        //Get: bookingtour

        [Authorize] //Ví dụ e muốn trang đặt tour cần phải đăng nhập.
        public async Task<ActionResult> BookingTour(long id) //Booking cho tour nào??? a hỏi bữa 1 lần rồi, tour id đâu?
        {
            //E cần thông tin user đang đăng nhập.
            var model = new BookingModelView();
            model.user = db.Users.Find(User.Identity.GetUserId());
            model.tour = db.Tours.Find(id);
            return View(model);
        }

        // GET: Tours/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }


        public PartialViewResult RelatedTours()
        {
            var model = new Models.ViewModels.TourPartialModelView();
            model.Tours = db.Tours.OrderByDescending(x => x.CreatedAt).Take(4).ToList();
            return PartialView("_RelatedToursPartial", model);
        }


        // GET: Tours/Create
        public ActionResult Create()
        {
            ViewBag.CategoryTourId = new SelectList(db.CategoryTours, "Id", "Name");
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Policy,Schedule,DateStart,Destination,DeparturePlace,CategoryTourId,Image,Price")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                tour.CreatedAt = DateTime.Now;
                tour.UpdatedAt = DateTime.Now;
                tour.DateStart = DateTime.Now;
                db.Tours.Add(tour);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryTourId = new SelectList(db.CategoryTours, "Id", "Name", tour.CategoryTourId);
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryTourId = new SelectList(db.CategoryTours, "Id", "Name", tour.CategoryTourId);
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Policy,Schedule,DateStart,Destination,DeparturePlace,CategoryTourId,Image,Price")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                tour.UpdatedAt = DateTime.Now;
                db.Entry(tour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryTourId = new SelectList(db.CategoryTours, "Id", "Name", tour.CategoryTourId);
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = await db.Tours.FindAsync(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tour tour = await db.Tours.FindAsync(id);
            db.Tours.Remove(tour);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
