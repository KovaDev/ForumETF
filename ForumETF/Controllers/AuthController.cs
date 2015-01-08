using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumETF.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ForumETF.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public AuthController() : this(Startup.UserManagerFactory.Invoke())
        {
            
        }

        public AuthController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        // GET: Auth
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LogIn(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(); // ako je doslo do problema, forma se ponovo prikazuje
            }

            AppUser user = await userManager.FindAsync(model.UserName, model.Password);

            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                //GetAuthenticationManager().SignIn(identity);
                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // klasa AppUser nasledjuje IdentityUser i samim tim ima sve njene propertije
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                //UserName = model.Email,
                Country = model.Country
            };

            var result = await userManager.CreateAsync(user, model.Password);

            
            if (result.Succeeded)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                //GetAuthenticationManager().SignIn(identity);
                //authManager.SignIn(identity);

                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }


            return View();
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("index", "home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }

            base.Dispose(disposing);
        }
        
    }
}