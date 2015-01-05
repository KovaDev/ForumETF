using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext db = null;
        private UserManager<AppUser> manager = null;

        public UserRepository()
        {
            db = new AppDbContext();
            manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
        }

        public AppUser GetUser(string username)
        {
            var user = manager.FindByName(username);

            return user;
        }
    }
}