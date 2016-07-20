using System;
using System.Collections.Generic;
using System.Reflection;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IReflectionService
    {
        List<Assembly> GetAssemblies();
        bool InterfaceFilter(Type typeObj, Object criteriaObj);
    }
}
