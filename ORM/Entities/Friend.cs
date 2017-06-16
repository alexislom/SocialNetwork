
namespace ORM.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? FriendUserId { get; set; }
        public bool Confirmed { get; set; }

        public virtual User User { get; set; }
        public virtual User FriendUser { get; set; }
    }
}
