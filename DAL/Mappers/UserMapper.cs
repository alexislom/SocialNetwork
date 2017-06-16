using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this User ormUser)
        {
            if (ormUser == null)
                return null;

            return new DalUser()
            {
                Id = ormUser.Id,
                ProfileId = ormUser.ProfileId,
                Password = ormUser.Password,
                Email = ormUser.Email,
                RoleId = ormUser.RoleId,
                UserProfile = ormUser.UserProfile.ToDalUserProfile(),
                UserName = ormUser.UserName
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;

            return new User()
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserProfile = dalUser.UserProfile.ToOrmUserProfile(),
                UserName = dalUser.UserName
            };
        }
    }
}
