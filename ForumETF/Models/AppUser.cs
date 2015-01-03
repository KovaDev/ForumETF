﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}