using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ForumETF.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Country { get; set; }
    }
}