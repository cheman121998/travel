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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            ViewBag.numberUsers = db.Users.Count();
            ViewBag.numberPlaces = db.Posts.Where(x => x.Category.Type == "Place").Count();
            ViewBag.numberFood = db.Posts.Where(x => x.Category.Type == "Food").Count();
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
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTour([Bind(Include = "Id,Name,Policy,Schedule,DateStart,Destination,DeparturePlace,CategoryTourId,Image,Price,CreatedAt")] Tour tour)
        {

            if (ModelState.IsValid)
            {
                tour.UpdatedAt = DateTime.Now;
                db.Entry(tour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Tour");
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
            db.Tours.Remove(tour);
            await db.SaveChangesAsync();
            return RedirectToAction("Tour");
        }

        public async Task<ActionResult> Post()
        {
            var posts = db.Posts.Include(t => t.Category);
            return View(await posts.ToListAsync());
        }

        // GET: Posts/Create
        public ActionResult CreatePost()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", "Image");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> CreatePost([Bind(Include = "Id,Title,Content,CategoryId,Image")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.Now;
                post.UpdatedAt = DateTime.Now;
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Post");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId, "Image");
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> EditPost(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId, "Image");
            return View(post);
        }



        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost([Bind(Include = "Id,Title,Content,CreatedAt,UpdatedAt,CategoryId,Image")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UpdatedAt = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Post");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", post.CategoryId, "Image");
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<ActionResult> DeletePost(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Post");
        }
     
        public async Task<ActionResult> User()
        {
            return View(await db.Users.ToListAsync());
        }


        public async Task<ActionResult> CategoryPost()
        {
            return View(await db.Categories.ToListAsync());
        }


        public ActionResult CreateCategory()
        {
            ViewBag.CategoryId = new SelectList("Id", "Name", "Type");
            return View();
        }

        [HttpPost]
       
        public async Task<ActionResult> CreateCategory([Bind(Include = "Id,Name,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
               
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryPost");
            }

            ViewBag.CategoryTourId = new SelectList("Id", "Name", "Type");
            return View(category);
        }

        public async Task<ActionResult> EditCategory(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category categoryPost = await db.Categories.FindAsync(id);
            if (categoryPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList("Id", "Name", "Type");
            return View(categoryPost);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory([Bind(Include = "Id,Name,Type")] Category categoryPost)
        {

            if (ModelState.IsValid)
            {
                
                db.Entry(categoryPost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryPost");
            }
            ViewBag.CategoryTourId = new SelectList("Id", "Name", "Type");

            return View(categoryPost);
        }

        public async Task<ActionResult> DeleteCategory(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryPost");
        }


        public async Task<ActionResult> CategoryTour()
        {
            return View(await db.CategoryTours.ToListAsync());
        }


        public ActionResult CreateCategoryTour()
        {
            ViewBag.CategoryTourId = new SelectList("Id", "Name");
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> CreateCategoryTour([Bind(Include = "Id,Name")] CategoryTour categoryTour)
        {
            if (ModelState.IsValid)
            {

                db.CategoryTours.Add(categoryTour);
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryTour");
            }

            ViewBag.CategoryTourId = new SelectList("Id", "Name");
            return View(categoryTour);
        }

        public async Task<ActionResult> EditCategoryTour(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryTour categoryTour = await db.CategoryTours.FindAsync(id);
            if (categoryTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryTourId = new SelectList("Id", "Name");
            return View(categoryTour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategoryTour([Bind(Include = "Id,Name")] CategoryTour categoryTour)
        {

            if (ModelState.IsValid)
            {

                db.Entry(categoryTour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryTour");
            }
            ViewBag.CategoryTourId = new SelectList("Id", "Name");

            return View(categoryTour);
        }

        public async Task<ActionResult> DeleteCategoryTour(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryTour categoryTour = await db.CategoryTours.FindAsync(id);
            if (categoryTour == null)
            {
                return HttpNotFound();
            }
            db.CategoryTours.Remove(categoryTour);
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryTour");
        }
    }
}

