﻿using System;
using System.Collections.Generic;
using ForumETF.Models;

namespace ForumETF.ViewModels
{
    public class PostDetailsViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public DateTime CreatedAt { get; set; }

        public AppUser User { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Answer> Answers { get; set; }
        public List<PostAttachment> Attachments { get; set; }
    }
}