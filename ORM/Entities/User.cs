using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            Friends = new HashSet<Friend>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }

        public int RoleId { get; set; }
        public int? ProfileId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
