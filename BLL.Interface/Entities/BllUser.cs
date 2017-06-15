using BLL.Interface.Interfaces;

namespace BLL.Interface.Entities
{
    public class BllUser : IEntityBLL
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProfileId { get; set; }
        public int RoleId { get; set; }
        public BllUserProfile UserProfile { get; set; }
    }
}
