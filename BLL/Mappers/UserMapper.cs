using BLL.Interfaces.Entities;
using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this BllUser bllUser)
        {
            if (bllUser == null)
                return null;
            var dalUser = new DalUser()
            {
                Id = bllUser.Id,
                ProfileId = bllUser.ProfileId,
                Password = bllUser.Password,
                Email = bllUser.Email,
                RoleId = bllUser.RoleId,
                UserProfile = bllUser.UserProfile.ToDalUserProfile(),
                UserName = bllUser.UserName
            };
         
            return dalUser;
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;
            return new BllUser()
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserProfile = dalUser.UserProfile.ToBllUserProfile(),
                UserName = dalUser.UserName
            };
        }
    }
}
