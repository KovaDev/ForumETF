using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.ViewModels
{
    public class UserEditViewModel
    {
        // licni podaci
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Index { get; set; }
        public string Address { get; set; }
        public HttpPostedFileBase AvatarUrl { get; set; }

        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedInUrl { get; set; }
    }
}