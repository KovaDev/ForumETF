using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class Answer
    {
        public Answer()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsApproved = false;
        }

        public int AnswerId { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; }
        public int Votes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual AppUser User { get; set; }
        public virtual Post Post { get; set; }
    }
}