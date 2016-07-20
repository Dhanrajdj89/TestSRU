using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IGlobalPermissionForRoleService
    {
        GlobalPermissionForRole Add(GlobalPermissionForRole permissionForRole);
        void Delete(GlobalPermissionForRole permissionForRole);
        GlobalPermissionForRole CheckExists(GlobalPermissionForRole permissionForRole);
        Dictionary<Permission, GlobalPermissionForRole> GetAll(MembershipRole role);
        Dictionary<Permission, GlobalPermissionForRole> GetAll();
        GlobalPermissionForRole Get(Guid permId, Guid roleId);
        GlobalPermissionForRole Get(Guid permId);
        void UpdateOrCreateNew(GlobalPermissionForRole globalPermissionForRole);
    }
}
