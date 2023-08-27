using System.ComponentModel.DataAnnotations;

namespace Shop_Site.Models.ViewModel
{
    public class ForgotViewModel
    {
        [Required]
        public string Email { get; set; } = null!;
    }
}
