using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models.Message
{
    public class MessagesViewModel
    {
        public int UserId { get; set; }
        public int InterlocutorId { get; set; }
        public IEnumerable<MessageModel> Messages { get; set; }
    }
}