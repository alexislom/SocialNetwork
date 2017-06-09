﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? Age
        {
            get
            {
                if (DateOfBirth == null)
                    return null;

                int age = DateTime.Now.Year - DateOfBirth.Value.Year;

                if (DateTime.Now.Month < DateOfBirth.Value.Month || 
                    (DateTime.Now.Month == DateOfBirth.Value.Month && DateTime.Now.Day < DateOfBirth.Value.Day))
                {
                    age--;
                }

                return age;
            }
            set { }
        }
        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }

        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
