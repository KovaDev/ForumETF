using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumETF.Models;

namespace ForumETF.ViewModels
{
    public class PostDetailsViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
    }
}