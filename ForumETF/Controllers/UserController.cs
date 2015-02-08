using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ForumETF.Models;
using ForumETF.Repositories;
using ForumETF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        [Route("User/Profile/{username}")]
        [ActionName("Profile")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UserProfile(string username)
        {
            AppUser user = manager.FindByName(username);

            UserViewModel viewModel = new UserViewModel()
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = user.Department,
                Address = user.Address,
                PhoneNumber = user.Phone,
                MobilePhone = user.MobilePhone,
                FacebookUrl = user.FacebookUrl,
                TwitterUrl = user.TwitterUrl,
                LinkedInUrl = user.LinkedinUrl
            };

            var postsCount = db.Posts.Count(p => p.User.UserName == user.UserName);
            
            ViewBag.PostsCount = postsCount;

            return View("Details", viewModel);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public PartialViewResult Details(string username)
        {
            //var user = _repo.GetUser(username);

            //return PartialView("_Details", user);
            return null;
        }

        //[HttpGet]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //public PartialViewResult Edit()
        //{
        //    AppUser user = manager.FindById(User.Identity.GetUserId());

        //    UserEditViewModel model = new UserEditViewModel
        //    {
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Department = user.Department,
        //        Address = user.Address
        //    };

        //    return PartialView("_Edit", model);
        //}

        [HttpGet]
        [Route("User/Profile/{username}/Edit")]
        public ActionResult Edit(string username)
        {
            AppUser user = manager.FindByName(username);

            UserViewModel viewModel = new UserViewModel()
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = user.Department,
                Address = user.Address,
                PhoneNumber = user.Phone,
                MobilePhone = user.MobilePhone,
                FacebookUrl = user.FacebookUrl,
                TwitterUrl = user.TwitterUrl,
                LinkedInUrl = user.LinkedinUrl
            };

            return View(viewModel);
        }


        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        [Route("User/Profile/{username}/Edit")]
        public ActionResult Edit(UserViewModel model)
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            if (model.Avatar != null && model.Avatar.ContentLength > 0)
            {
                var filename = Path.GetFileName(model.Avatar.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/ProfilePictures"), filename);
                model.Avatar.SaveAs(path);
                user.AvatarUrl = path;
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Department = model.Department;
            user.Address = model.Address;
            user.Email = model.Email;
            user.Phone = model.PhoneNumber;
            user.MobilePhone = model.MobilePhone;
            user.FacebookUrl = model.FacebookUrl;
            user.TwitterUrl = model.TwitterUrl;
            user.LinkedinUrl = model.LinkedInUrl;

            IdentityResult result = manager.Update(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Profile", "User", new { username = User.Identity.Name });
        }

        [HttpGet]
        [Route("User/Profile/{username}/Posts")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult GetPublishedPosts()
        {
            AppUser user = manager.FindById(User.Identity.GetUserId());

            var posts = (from p in db.Posts
                        where p.User.UserName == user.UserName
                        select p
                        ).OrderByDescending(p => p.CreatedAt).ToList();

            UserViewModel viewModel = new UserViewModel()
            {
                UserName = user.UserName,
                AvatarUrl = user.AvatarUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Department = user.Department,
                Address = user.Address,
                PhoneNumber = user.Phone,
                MobilePhone = user.MobilePhone,
                FacebookUrl = user.FacebookUrl,
                TwitterUrl = user.TwitterUrl,
                LinkedInUrl = user.LinkedinUrl,
                Posts = posts
            };

            return View("PublishedPosts", viewModel);
        }

        public ActionResult Modal(int postId)
        {
            return Content("Uspjesno obrisan post br ", postId.ToString());
        }
    }
}