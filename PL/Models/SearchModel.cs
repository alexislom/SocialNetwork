using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class SearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        [Required(ErrorMessage = "Set up gender")]
        public bool? Gender { get; set; }
        public string City { get; set; }
    }
}