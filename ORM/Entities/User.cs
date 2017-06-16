
namespace ORM.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProfileId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
