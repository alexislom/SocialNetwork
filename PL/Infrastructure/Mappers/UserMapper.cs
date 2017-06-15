using BLL.Interface.Entities;
using PL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel ToMvcUser(this BllUser bllUser)
        {
            if (bllUser == null)
                return null;

            return new UserViewModel()
            {
                Id = bllUser.Id,
                Email = bllUser.Email,
                Password = bllUser.Password,
                ProfileId = bllUser.ProfileId,
                RoleId = bllUser.RoleId,
                UserName = bllUser.UserName
            };
        }
    }
}