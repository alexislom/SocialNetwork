using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? RequestDate { get; set; }
        [ForeignKey("ToUser")]
        public int? UserFromId { get; set; }
        [ForeignKey("FromUser")]
        public int? UserToId { get; set; }
        public bool? IsConfirmed { get; set; }

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
