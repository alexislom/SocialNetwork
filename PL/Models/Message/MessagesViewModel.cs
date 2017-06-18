using System.Collections.Generic;
using PL.Models.User;

namespace PL.Models.Message
{
    public class MessagesViewModel
    {
        //public int UserId { get; set; }
        //public int InterlocutorId { get; set; }
        public UserViewModel FromUser { get; set; }
        public UserViewModel ToUser { get; set; }
        public IEnumerable<MessageModel> Messages { get; set; }
    }
}