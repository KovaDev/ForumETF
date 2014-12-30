using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.Controllers
{
    public class AnswerController : Controller
    {
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public AnswerController()
        {
            db = new AppDbContext();
            manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
        }

        // GET: Answer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Create(int post, string answer)
        {
            string content = WebUtility.HtmlDecode(answer);

            var currentUser = manager.FindById(User.Identity.GetUserId());
            var existing_post = db.Posts.Find(post);

            Answer new_answer = new Answer
            {
                Content = content,
                Post = existing_post,
                User = currentUser
            };

            db.Answers.Add(new_answer);
            db.SaveChanges();

            return PartialView("_NewAnswer", new_answer);
        }
    }
}