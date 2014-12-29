using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Controllers
{
    public class CommentController : Controller
    {
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public CommentController()
        {
            db = new AppDbContext();
            manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
        }

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Create(int post, string commentContent)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var existing_post = db.Posts.Find(post);

            Comment comment = new Comment
            {
                Content = commentContent,
                IsApproved = true,
                User = currentUser,
                Post = existing_post
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            return PartialView("_NewComment", comment);
        }

    }
}