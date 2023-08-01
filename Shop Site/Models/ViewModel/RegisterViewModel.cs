using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop_Site.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(5)]
        public string FullName { get; set; } = null!;
        [Required]
        [MinLength(4)]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

