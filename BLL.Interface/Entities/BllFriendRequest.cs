using BLL.Interface.Interfaces;
using System;

namespace BLL.Interface.Entities
{
    public class BllFriendRequest : IEntityBLL
    {
        public int Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? UserFromId { get; set; }
        public int? UserToId { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
