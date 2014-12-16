using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public bool IsApproved { get; set; }

        public virtual AppUser User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}