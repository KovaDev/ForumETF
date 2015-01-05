using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumETF.Repositories;
using ForumETF.Models;

namespace ForumETF.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repo = null;

        public UserController()
        {
            _repo = new UserRepository();
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
            var user = _repo.GetUser(username);

            return View(user);
        }

        public PartialViewResult Details()
        {
            return PartialView("_Details");
        }

        [HttpGet]
        public PartialViewResult Edit()
        {
            return PartialView("_Edit");
        }

        [HttpPost]
        public PartialViewResult Edit(AppUser model)
        {
            // kod koji treba da azurira korisnicke podatke
            // model state modified
            
            return null;
        }
    }
}