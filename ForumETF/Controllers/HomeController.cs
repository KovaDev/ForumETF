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
using System.Web.UI;
using ForumETF.Repositories;

namespace ForumETF.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext db = null;
        private PostRepository repository = null; 

        public HomeController ()
	    {
            db = new AppDbContext();
            repository = new PostRepository();
	    }

        // GET: Home
        [HttpGet]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None", Location = OutputCacheLocation.ServerAndClient)]
        //[OutputCache(Duration=60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Index(int? page, string searchTerm = null)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var posts = db.Posts.Where(p => p.Title.Contains(searchTerm) || searchTerm == null)
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Posts", posts);
            }

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

        //[OutputCache(Duration=60)]
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.ServerAndClient)]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult MostPopularPosts(int? page)
        {
            //int pageSize = 10;
            //int pageNumber = (page ?? 1);

            //var posts = db.Posts.OrderByDescending(p => p.Votes).ToPagedList(pageNumber, pageSize);
            var posts = repository.GetMostPopularPosts(page);

            return PartialView("_Posts", posts);
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