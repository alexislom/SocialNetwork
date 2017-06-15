using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.Profile
{
    public class ProfileEditModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "The surname must contain at least {2} characters", MinimumLength = 4)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday date")]
        public DateTime? DateOfBirth { get; set; }

        public string City { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string MobilePhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string Status { get; set; }
    }
}