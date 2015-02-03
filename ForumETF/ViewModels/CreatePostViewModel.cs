using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.ViewModels
{
    public class CreatePostViewModel
    {
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Title { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public int Votes { get; set; }

        public string Tags { get; set; }

        public SelectList Categories { get; set; }

        [Required(ErrorMessage = "Morate izabrati neku od ponuđenih kategorija!")]
        public int SelectedId { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

    }
}