using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumETF.Repositories;
using ForumETF.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ForumETF.ViewModels;

namespace ForumETF.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repo = null;
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public UserController()
        {
            _repo = new UserRepository();
            db = new AppDbContext();
            manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
        }

        // GET: User
        public ActionResult Index(string username)
        {
            return View();
        }

        [Route("User/Profile/{username}")]
        [ActionName("Profile")]
        public ActionResult UserProfile(string username)
        {
            //AppUser user = _repo.GetUser(username);
            AppUser user = manager.FindById(User.Identity.GetUserId());
            //var postsCount = db.Posts.Where(p => p.User == user).Count();
            
            ViewBag.PostsCount = 15;

            return View(user);
        }

        public PartialViewResult Details(string username)
        {
            var user = _repo.GetUser(username);

            return PartialView("_Details", user);
        }

        [HttpGet]
        public PartialViewResult Edit()
        {
            return PartialView("_Edit");
        }

        [HttpPost]
        public PartialViewResult Edit(AppUser model)
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Department = model.Department;
            user.Address = model.Address;
            user.Email = model.Email;

            IdentityResult result = manager.Update(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return PartialView("_Details", user);
        }

        [HttpGet]
        public PartialViewResult GetPublishedPosts()
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            var posts = (from p in db.Posts
                        where p.User.UserName == user.UserName
                        select p
                        ).OrderByDescending(p => p.CreatedAt).ToList();

            return PartialView("_PublishedPosts", posts);
        }
    }
}