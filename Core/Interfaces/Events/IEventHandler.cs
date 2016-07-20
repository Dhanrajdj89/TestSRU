using SportsRUsApp.Core.Events;

namespace SportsRUsApp.Core.Interfaces.Events
{
    public interface IEventHandler
    {
        void RegisterHandlers(EventManager theEventManager);
    }
}
