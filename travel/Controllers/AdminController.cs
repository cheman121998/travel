using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using travel.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace travel.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            ViewBag.numberUsers = db.Users.Count();
            ViewBag.numberPlaces = db.Posts.Where(x=>x.Category.Type=="Place").Count();
            ViewBag.numberFood = db.Posts.Where(x=>x.Category.Type=="Food").Count();
            ViewBag.numberTour = db.Tours.Count();
            return View();
        }

        public async Task<ActionResult> Tour()
        {
            var tours = db.Tours.Include(t => t.CategoryTour);
            return View(await tours.ToListAsync());
        }

        public ActionResult CreateTour()
        {
            ViewBag.CategoryTourId = new SelectList(db.CategoryTours, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTour([Bind(Include = "Id,Name,Policy,Schedule,DateStart,Destination,DeparturePlace,CategoryTourId,Image,Price")] Tour tour)
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
        public async Task<ActionResult> EditTour(long? id)
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
        public async Task<ActionResult> EditTour([Bind(Include = "Id,Name,Policy,Schedule,DateStart,Destination,DeparturePlace,CategoryTourId,Image,Price")] Tour tour)
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
        public async Task<ActionResult> DeleteTour(long? id)
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

    }
}
