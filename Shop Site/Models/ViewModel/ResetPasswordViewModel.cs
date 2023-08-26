﻿using System.ComponentModel.DataAnnotations;

namespace Shop_Site.Models.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Password { get; set; } = null!;
        [Compare("Password",ErrorMessage= "The Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
