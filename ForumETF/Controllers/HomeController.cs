using System.Linq;
using System.Web.Mvc;
using ForumETF.Models;
using ForumETF.Repositories;
using PagedList;

namespace ForumETF.Controllers
{   
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IPostRepository _repo; 

        public HomeController ()
	    {
            _db = new AppDbContext();
            _repo = new PostRepository(_db);
	    }

        // GET: Home
        [HttpGet]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None", Location = OutputCacheLocation.ServerAndClient)]
        //[OutputCache(Duration=60, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Index(int? page, string searchTerm = null)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            var posts = _db.Posts.Where(p => p.Title.Contains(searchTerm) || searchTerm == null)
                .OrderByDescending(p => p.CreatedAt)
                .ToPagedList(pageNumber, pageSize);
            /*
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Posts", posts);
            }*/

            return View(posts);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult CategoriesWidget()
        {
            var categories = _db.Categories.ToList();

            return PartialView("_CategoriesWidget", categories);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult TopTenPostsWidget()
        {
            return PartialView("_TopTenPostsWidget", _repo.GetTop10Posts());
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
            var posts = _repo.GetMostPopularPosts(page);

            return PartialView("_Posts", posts);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}