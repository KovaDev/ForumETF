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
using ForumETF.ViewModels;

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
            var categories = db.Categories.ToList();

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Izaberite kategoriju", Value = "0" });

            int counter = 1;

            foreach (var c in categories)
            {
                items.Add(new SelectListItem { Text = c.CategoryName, Value = counter.ToString()});
                counter++;
            }

            ViewBag.Categories = items;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePostViewModel model)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            ICollection<Tag> tagList = new List<Tag>();
            
            string[] tagNames = model.Tags.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string tagName in tagNames)
            {
                tagList.Add(GetTag(tagName));
            }

            //Category cat = await db.Categories.FindAsync(1);
            Category cat = db.Categories.Where(c => c.CategoryName == model.Category).FirstOrDefault();

            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Votes = model.Votes,
                IsApproved = false,
                CreatedAt = DateTime.Now,
                User = currentUser,
                Category = cat,
                Tags = tagList
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

        private Tag GetTag(string tagName)
        {
            return db.Tags.Where(t => t.TagName == tagName).FirstOrDefault() ?? new Tag { TagName = tagName };
        }

    }
}