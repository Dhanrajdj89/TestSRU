using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IRoleService
    {
        IList<MembershipRole> AllRoles();
        void Delete(MembershipRole role);
        MembershipRole GetRole(string rolename, bool removeTracking = false);
        MembershipRole GetRole(Guid id);
        IList<MembershipUser> GetUsersForRole(string roleName);
        MembershipRole CreateRole(MembershipRole role);
        PermissionSet GetPermissions(Category category, MembershipRole role);
    }
}
