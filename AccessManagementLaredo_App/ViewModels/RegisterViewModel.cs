using System.ComponentModel.DataAnnotations;

namespace AccessManagementLaredo_App.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your role")]
        [DataType(DataType.Text)]
        [Display(Name = "Role")]
        [MaxLength(30)]
        public string Role { get; set; }

        //[Required(ErrorMessage = "Please enter an email")]
        [DataType(DataType.Text)]
        [Display(Name = "Company Name")]
        //[EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Please enter an email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
