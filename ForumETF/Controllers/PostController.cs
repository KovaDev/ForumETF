using ForumETF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ForumETF.Controllers
{
    public class PostController : Controller
    {
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public PostController ()
	    {
            this.db = new AppDbContext();
            this.manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
    	}

        // GET: Post
        public ActionResult Index()
        {
            //return View(await db.Posts.ToListAsync());
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Create(Post model)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Votes = model.Votes,
                IsApproved = false,
                User = currentUser
            };

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            

            return View(post);
        }

    }
}