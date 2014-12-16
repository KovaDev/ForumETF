using ForumETF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net;

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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Post model)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Votes = model.Votes,
                IsApproved = false,
                CreatedAt = DateTime.Now,
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

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = await db.Posts.FindAsync(id);

            if (post == null)
            {
                //return HttpNotFound();
                return View("404");
            }

            return View(post);
        }

    }
}