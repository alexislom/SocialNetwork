using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.User
{
    public class RegisterViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter login")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter last name")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Set up your gender")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect Email")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match")]
        public string PasswordConfirm { get; set; }

        //[Required(ErrorMessage = "The code from the image is required")]
        //[Display(Name = "Enter the code from the image")]
        //public string Captcha { get; set; }
    }
}