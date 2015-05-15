using System.Collections.Generic;
using System.Web;
using ForumETF.Models;

namespace ForumETF.ViewModels
{
    public class UserViewModel
    {
        // licni podaci
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Index { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public HttpPostedFileBase Avatar { get; set; }

        public string WebSite { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedinUrl { get; set; }

        public List<Post> Posts { get; set; }
    }
}