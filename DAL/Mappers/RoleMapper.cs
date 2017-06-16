using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this Role ormRole)
        {
            if (ormRole == null)
                return null;

            return new DalRole
            {
                Id = ormRole.Id,
                Name = ormRole.Name
            };
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null)
                return null;

            return new Role
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
