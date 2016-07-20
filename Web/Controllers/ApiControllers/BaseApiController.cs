using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Core.Interfaces.UnitOfWork;
using SportsRUsApp.ViewModels.Application;
using System.Web.Http;

namespace SportsRUsApp.Web.Controllers.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected IMembershipService MembershipService { get; set; } = ServiceFactory.Get<IMembershipService>();
        protected IRoleService RoleService { get; set; } = ServiceFactory.Get<IRoleService>();
        protected ILocalizationService LocalizationService { get; set; } = ServiceFactory.Get<ILocalizationService>();
        protected IUnitOfWorkManager UnitOfWorkManager { get; set; } = ServiceFactory.Get<IUnitOfWorkManager>();
        protected ILoggingService LoggingService { get; set; } = ServiceFactory.Get<ILoggingService>();
        protected ISettingsService SettingsService { get; set; } = ServiceFactory.Get<ISettingsService>();
    }
}
