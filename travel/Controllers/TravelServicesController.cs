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
    public class TravelServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TravelServices
        public async Task<ActionResult> Index()
        {
            var travelServices = db.TravelServices.Include(t => t.User);
            return View(await travelServices.ToListAsync());
        }

        // GET: TravelServices/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelService travelService = await db.TravelServices.FindAsync(id);
            if (travelService == null)
            {
                return HttpNotFound();
            }
            return View(travelService);
        }

        // GET: TravelServices/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: TravelServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,TaxCode,Represent,Phone,Email,Website,Nnkd,UserId")] TravelService travelService)
        {
            if (ModelState.IsValid)
            {
                db.TravelServices.Add(travelService);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", travelService.UserId);
            return View(travelService);
        }

        // GET: TravelServices/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelService travelService = await db.TravelServices.FindAsync(id);
            if (travelService == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", travelService.UserId);
            return View(travelService);
        }

        // POST: TravelServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,TaxCode,Represent,Phone,Email,Website,Nnkd,UserId")] TravelService travelService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelService).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", travelService.UserId);
            return View(travelService);
        }

        // GET: TravelServices/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelService travelService = await db.TravelServices.FindAsync(id);
            if (travelService == null)
            {
                return HttpNotFound();
            }
            return View(travelService);
        }

        // POST: TravelServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            TravelService travelService = await db.TravelServices.FindAsync(id);
            db.TravelServices.Remove(travelService);
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
