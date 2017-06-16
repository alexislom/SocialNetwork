using System;

namespace DAL.Interfaces.DTO
{
    public class DalFriendRequest : IEntity
    {
        public int Id { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? UserFromId { get; set; }
        public int? UserToId { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
