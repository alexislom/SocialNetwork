﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public string LastName { get; set; }
        public int? PhotoId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public bool? Gender { get; set; }

    }
}