using System.Collections.Generic;
using System.Web.Mvc;

namespace PL.Models.User
{
    public class UsersEditModel
    {
        public UsersEditModel()
        {
            Users = new HashSet<UserViewModel>();
            Roles = new HashSet<SelectListItem>();
        }

        public IEnumerable<UserViewModel> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public string NewRole { get; set; }
    }
}