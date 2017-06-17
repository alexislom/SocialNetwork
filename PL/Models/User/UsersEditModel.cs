using System.Collections.Generic;
using System.Web.Mvc;

namespace PL.Models.User
{
    public class UsersEditModel
    {
        public UserViewModel User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public string NewRole { get; set; }
    }
}