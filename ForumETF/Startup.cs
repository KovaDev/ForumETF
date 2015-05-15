using System;
using ForumETF;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace ForumETF
{
    public class Startup
    {
        public static Func<UserManager<AppUser>> UserManagerFactory { get; private set; }

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


        }
    }
}
