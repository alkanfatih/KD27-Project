using System.ComponentModel.DataAnnotations;

namespace UIStoreAppMvc.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tam ad alanı boş geçilemez!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email alanı boş geçilemez!")]
        [EmailAddress(ErrorMessage = "Hatalı email formatı")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
