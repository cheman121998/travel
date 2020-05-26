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

namespace travel.Controllers
{
    public class BookingTourController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TourDetails
        public async Task<ActionResult> Index()
        {
            var tourDetails = db.TourDetails.Include(t => t.Tour).Include(t => t.User);
            return View(await tourDetails.ToListAsync());
        }

        // GET: TourDetails/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTour tourDetail = await db.TourDetails.FindAsync(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            return View(tourDetail);
        }

        // GET: TourDetails/Create
        public ActionResult Create()
        {
            ViewBag.TourId = new SelectList(db.Tours, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: TourDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountAdult,CountChild,Price,DateBook,UserId,TourId")] BookingTour tourDetail)
        {
            if (ModelState.IsValid)
            {
                db.TourDetails.Add(tourDetail);
                await db.SaveChangesAsync();
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.TourId = new SelectList(db.Tours, "Id", "Name", tourDetail.TourId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tourDetail.UserId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: TourDetails/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTour tourDetail = await db.TourDetails.FindAsync(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TourId = new SelectList(db.Tours, "Id", "Name", tourDetail.TourId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tourDetail.UserId);
            return View(tourDetail);
        }

        // POST: TourDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountAdult,CountChild,Price,DateBook,UserId,TourId")] BookingTour tourDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tourDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TourId = new SelectList(db.Tours, "Id", "Name", tourDetail.TourId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", tourDetail.UserId);
            return View(tourDetail);
        }

        // GET: TourDetails/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTour tourDetail = await db.TourDetails.FindAsync(id);
            if (tourDetail == null)
            {
                return HttpNotFound();
            }
            return View(tourDetail);
        }

        // POST: TourDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            BookingTour tourDetail = await db.TourDetails.FindAsync(id);
            db.TourDetails.Remove(tourDetail);
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
