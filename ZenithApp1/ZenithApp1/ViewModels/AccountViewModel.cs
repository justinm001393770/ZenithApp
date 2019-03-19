using System.ComponentModel.DataAnnotations;

namespace ZenithApp1.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(25, ErrorMessage = "The {0} must be between 4 and 25 characters in length.", MinimumLength = 4)]
        public string Username { get; set; }

        [Display(Name = "Phone")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm email")]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        public string ConfirmEmail { get; set; }
    }
}