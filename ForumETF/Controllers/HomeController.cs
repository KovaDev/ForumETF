using ForumETF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ForumETF.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var posts = await db.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();

            return View(posts);
        }

        [ChildActionOnly]
        public ActionResult CategoriesWidget()
        {
            var categories = db.Categories.ToList();

            return PartialView("_CategoriesWidget", categories);
        }

        //[ChildActionOnly]
        [Route("Tag/Tagovi")]
        public ActionResult TagsWidget()
        {
            return Content("Route test");
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