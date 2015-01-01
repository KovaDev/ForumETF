using ForumETF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace ForumETF.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var posts = db.Posts.OrderByDescending(p => p.CreatedAt).ToPagedList(pageNumber, pageSize);

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