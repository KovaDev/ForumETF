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
using System.Web.UI;
using System.IO;

namespace ForumETF.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UserProfile(string username)
        {
            //AppUser user = _repo.GetUser(username);
            AppUser user = manager.FindById(User.Identity.GetUserId());
            var postsCount = db.Posts.Where(p => p.User.UserName == user.UserName).Count();
            
            ViewBag.PostsCount = postsCount;

            return View(user);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult Details(string username)
        {
            var user = _repo.GetUser(username);

            return PartialView("_Details", user);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult Edit()
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            UserEditViewModel model = new UserEditViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Department = user.Department,
                Address = user.Address
            };

            return PartialView("_Edit", model);
        }

        
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Edit(UserEditViewModel model)
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            if (model.AvatarUrl.ContentLength > 0)
            {
                var filename = Path.GetFileName(model.AvatarUrl.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/ProfilePictures"), filename);
                model.AvatarUrl.SaveAs(path);
                user.AvatarUrl = path;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Department = model.Department;
            user.Address = model.Address;
            user.Email = model.Email;

            IdentityResult result = manager.Update(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            //return PartialView("_Details", user);
            return RedirectToAction("Profile", "User", new { username = User.Identity.Name });
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult GetPublishedPosts()
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            var posts = (from p in db.Posts
                        where p.User.UserName == user.UserName
                        select p
                        ).OrderByDescending(p => p.CreatedAt).ToList();

            return PartialView("_PublishedPosts", posts);
        }

        public ActionResult Modal(int postId)
        {
            return Content("Uspjesno obrisan post br ", postId.ToString());
        }
    }
}