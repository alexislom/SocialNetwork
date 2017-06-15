using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Models.Message;

namespace PL.Models.Profile
{
    public class DialogViewModel
    {
        public DialogProfile InterLocutor { get; set; }

        public MessageModel LastMessage { get; set; }
    }
}