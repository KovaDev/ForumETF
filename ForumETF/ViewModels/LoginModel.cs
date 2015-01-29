using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Polje je obavezno !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Polje je obavezno !")]  
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}