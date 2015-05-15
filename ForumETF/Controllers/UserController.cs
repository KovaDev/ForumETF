using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
        private IUserRepository _repo;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _manager;

        public UserController()
        {
            _repo = new UserRepository();
            _db = new AppDbContext();
            _manager = new UserManager<AppUser>(new UserStore<AppUser>(_db));
        }

        [Route("User/Profile/{username}")]
        [ActionName("Profile")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UserProfile(string username)
        {
            var user = _manager.FindByName(username);
            var viewModel = MapDomainToViewModel(user);

            ViewBag.PostsCount = _db.Posts.Count(p => p.User.UserName == user.UserName);

            return View("Details", viewModel);
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
            var user = _manager.FindByName(username);
            var viewModel = MapDomainToViewModel(user);

            return View(viewModel);
        }


        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        [Route("User/Profile/{username}/Edit")]
        public ActionResult Edit(UserViewModel model)
        {
            var user = _manager.FindById(User.Identity.GetUserId());

            Mapper.CreateMap<UserViewModel, AppUser>();
            Mapper.Map(model, user);

            if (model.Avatar != null && model.Avatar.ContentLength > 0)
            {
                var filename = Path.GetFileName(model.Avatar.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/ProfilePictures"), filename);
                model.Avatar.SaveAs(path);
                user.AvatarUrl = path;
            }
            //UpdateProfilePicture(user, model);

            IdentityResult result = _manager.Update(user);
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Profile", "User", new { username = User.Identity.Name });
        }

        [HttpGet]
        [Route("User/Profile/{username}/Posts")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult GetPublishedPosts()
        {
            var user = _manager.FindById(User.Identity.GetUserId());

            var posts = (from p in _db.Posts
                        where p.User.UserName == user.UserName
                        select p
                        ).OrderByDescending(p => p.CreatedAt).ToList();

            var viewModel = MapDomainToViewModel(user);
            viewModel.Posts = _db.Posts.ToList();

            return View("PublishedPosts", viewModel);
        }

        public ActionResult Modal(int postId)
        {
            return Content("Uspjesno obrisan post br ", postId.ToString());
        }

        private void UpdateProfilePicture(AppUser user, UserViewModel viewModel)
        {
            if (viewModel.Avatar != null && viewModel.Avatar.ContentLength > 0)
            {
                var filename = Path.GetFileName(viewModel.Avatar.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/ProfilePictures"), filename);
                viewModel.Avatar.SaveAs(path);
                user.AvatarUrl = path;
            }
        }

        private UserViewModel MapDomainToViewModel(AppUser user)
        {
            Mapper.CreateMap<AppUser, UserViewModel>();
            var viewModel = Mapper.Map<UserViewModel>(user);
         
            return viewModel;
        }

        private AppUser MapViewModelToDomainModel(UserViewModel viewModel)
        {
            Mapper.CreateMap<UserViewModel, AppUser>();
            var user = Mapper.Map<AppUser>(viewModel);

            return user;
        }
    }
}