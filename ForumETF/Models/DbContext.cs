using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Models
{
    public class DbContext : IdentityDbContext<AppUser>
    {
        public DbContext()
            : base("DbContext")
        {
 
        }
    }
}