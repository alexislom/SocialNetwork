using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService => 
            (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService => 
            (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string username, string roleName)
        {
            var role = RoleService.GetById(UserService.GetOneByPredicate(u => u.UserName == username).RoleId);
            if (role.Name == roleName)
                return true;
            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[] { };
            var user = UserService.GetOneByPredicate(u => u.UserName == username);
            if (user != null)
            {
                var role = RoleService.GetById(user.RoleId);
                return new[] {role.Name};
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var role = new BllRole { Name = roleName };
            RoleService.Create(role);
        }

        #region NotImplementedMethods
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}