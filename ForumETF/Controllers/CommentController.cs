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

        public PartialViewResult Create(string comment_body, int post)
        {
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var existing_post = db.Posts.Find(post);

            Comment comment = new Comment
            {
                Content = comment_body,
                IsApproved = true,
                User = currentUser,
                Post = existing_post
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            var comments = db.Comments.ToList();

            return PartialView("_Comments", comments);
        }

    }
}