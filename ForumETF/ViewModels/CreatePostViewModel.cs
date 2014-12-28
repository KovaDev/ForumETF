using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumETF.Models;
using System.Web.Mvc;

namespace ForumETF.ViewModels
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public string Tags { get; set; }
        public SelectList Categories { get; set; }
        public int SelectedId { get; set; }
        
    }
}