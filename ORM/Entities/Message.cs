using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public DateTime? Date { get; set; }
        public bool ReadMessage { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        [ForeignKey("FromUserId")]
        public virtual UserProfile FromUser { get; set; }
        [ForeignKey("ToUserId")]
        public virtual UserProfile ToUser { get; set; }
    }
}
