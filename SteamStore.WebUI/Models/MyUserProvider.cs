using SteamStore.DAL.Concrete;
using SteamStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SteamStore.WebUI.Models
{
    public class MyUserProvider : RoleProvider
    {
        enum Roles
        {
            User,
            Admin
        }
        public override string[] GetRolesForUser(string username)
        {
            switch (username)
            {
                case "Admin":
                    return new[] { "Admin", "User" };
                default: return new[] { "User" };
            }
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            using (EFDbContext db = new EFDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login == username);

                if (user != null)
                {
                    return roleName == user.Role;
                }
                return false;
            }
        }
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}