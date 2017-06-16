﻿
namespace DAL.Interfaces.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? ProfileId { get; set; }
        public int RoleId { get; set; }
        public DalUserProfile UserProfile { get; set; }
    }
}
