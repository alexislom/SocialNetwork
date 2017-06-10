using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.Profile
{
    public class FullProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? PhotoId { get; set; }

        public string City { get; set; }

        public bool? Gender { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string Status { get; set; }

        public int EqualityToSearchObject(FullProfileViewModel obj)
        {
            int equalCoeff = 0;
            if (obj.FirstName == FirstName)
                equalCoeff++;
            if (obj.LastName == LastName)
                equalCoeff++;
            if (obj.City == City)
                equalCoeff++;
            if (obj.Gender == Gender)
                equalCoeff++;
            if (obj.NickName == NickName)
                equalCoeff++;
            return equalCoeff;
        }
    }
}