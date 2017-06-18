using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? HouseNumber { get; set; }
        public string CompanyOfWork { get; set; }
        public string Status { get; set; }
        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }

        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}

