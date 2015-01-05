using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public string About { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string GithubUrl { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<AnswerComment> AnswerComments { get; set; }
    }
}