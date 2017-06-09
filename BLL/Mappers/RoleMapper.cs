using DAL.Interfaces.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class RoleMapper
    {
        public static BllRole ToBllRole(this DalRole role)
        {
            var BllRole = new BllRole
            {
                Id = role.Id,
                Name = role.Name
            };
            return BllRole;
        }

        public static DalRole ToDalRole(this BllRole role)
        {
            var dalRole = new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
            return dalRole;
        }
    }
}
