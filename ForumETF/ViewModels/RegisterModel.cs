using System.ComponentModel.DataAnnotations;

namespace ForumETF.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Unesena adresa nije ispravna!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Polje je obavezno!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju!")]
        public string Password2 { get; set; }
    }
}