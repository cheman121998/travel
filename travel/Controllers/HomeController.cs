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
            return View(await db.Posts.ToListAsync());
        }

        //public actionresult index()
        //{
        //    return view();
        //}

        public ActionResult About()
        {
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