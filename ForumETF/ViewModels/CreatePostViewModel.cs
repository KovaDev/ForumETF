using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.ViewModels
{
    public class CreatePostViewModel
    {
        ///[Required(ErrorMessage = "Polje je obavezno!")]
        public string Title { get; set; }

        //[AllowHtml]
        //[Required(ErrorMessage = "Polje je obavezno")]
        [AllowHtml]
        public string Content { get; set; }

        public int Votes { get; set; }

        public string Tags { get; set; }

        //[Required(ErrorMessage = "Morate izabrati neku od ponuđenih kategorija!")]
        public SelectList Categories { get; set; }

        public int SelectedId { get; set; }

        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        //public CreatePostViewModel()
        //{
        //    Votes = 0;
        //}
    }
}