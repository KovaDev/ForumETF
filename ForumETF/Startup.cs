using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ForumETF.Models;

[assembly: OwinStartup(typeof(ForumETF.Startup))]

namespace ForumETF
{
    public class Startup
    {
        public static Func<UserManager<AppUser>> UserManagerFactory { get; private set; }
        //public static Func<RoleManager<AppRole>> RoleManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCookieAuthentication(new CookieAuthenticationOptions 
            { 
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login") // redirekcija na login akciju login kontrolera
            });

            UserManagerFactory = () =>
            {
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new AppDbContext()));

                userManager.UserValidator = new UserValidator<AppUser>(userManager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return userManager;
            };

            //RoleManagerFactory = () =>
            //{
            //    var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new AppDbContext()));

            //    return roleManager;
            //};

        }
    }
}
