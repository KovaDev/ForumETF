using System.Net;
using System.Web.Mvc;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            var existingPost = db.Posts.Find(post);

            Answer new_answer = new Answer
            {
                Content = content,
                Post = existingPost,
                User = currentUser
            };

            db.Answers.Add(new_answer);
            db.SaveChanges();

            return PartialView("_NewAnswer", new_answer);
        }
    }
}