using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models.Profile
{
    public class SearchUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public bool? Gender { get; set; }
        public string City { get; set; }

        public int CompareToObject(SearchUserModel obj)
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