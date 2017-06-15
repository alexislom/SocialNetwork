using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PL.Models.Profile;

namespace PL.Models.Message
{
    public class MessageModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime SendTime { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public DialogProfile Sender { get; set; }
        public DialogProfile Receiver { get; set; }
    }
}