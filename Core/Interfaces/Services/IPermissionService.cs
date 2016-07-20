using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IPermissionService
    {
        IEnumerable<Permission> GetAll();
        Permission Add(Permission permission);
        void Delete(Permission permission);
        Permission Get(Guid id);
    }
}
