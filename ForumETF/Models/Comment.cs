using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}