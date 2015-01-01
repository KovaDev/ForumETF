using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumETF.Models;

namespace ForumETF.Models
{
    public class AnswerComment
    {
        public int AnswerCommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsApproved { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Answer Answer { get; set; }
    }
}