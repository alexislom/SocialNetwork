using BLL.Interface.Interfaces;
using System;

namespace BLL.Interface.Entities
{
    public class BllUserProfile : IEntityBLL
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? PhotoId { get; set; }

        public virtual BllPhoto Photo { get; set; }

        public string City { get; set; }

        public bool? Gender { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Status { get; set; }
    }
}
