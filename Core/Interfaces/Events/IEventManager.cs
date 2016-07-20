using System.Collections.Generic;
using System.Reflection;
using SportsRUsApp.Core.Interfaces.Services;

namespace SportsRUsApp.Core.Interfaces.Events
{
    public interface IEventManager
    {        
        /// <summary>
        /// Use reflection to get all event handling classes. Call this ONCE.
        /// </summary>
        void Initialize(ILoggingService loggingService, List<Assembly> assemblies);

    }
}
