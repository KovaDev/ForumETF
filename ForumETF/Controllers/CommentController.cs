using System.Web.Mvc;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _manager;

        public CommentController()
        {
            _db = new AppDbContext();
            _manager = new UserManager<AppUser>(new UserStore<AppUser>(_db));
        }

        [HttpPost]
        public PartialViewResult Create(int? post, string commentContent)
        {
            var currentUser = _manager.FindById(User.Identity.GetUserId());
            var existingPost = _db.Posts.Find(post);

            var comment = new Comment
            {
                Content = commentContent,
                IsApproved = true,
                User = currentUser,
                Post = existingPost
            };

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return PartialView("_Comments", comment);
        }

    }
}