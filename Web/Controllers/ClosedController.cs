using System.Web.Mvc;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Core.Interfaces.UnitOfWork;

namespace SportsRUsApp.Web.Controllers
{
    public partial class ClosedController : BaseController
    {
        public ClosedController(ILoggingService loggingService, IUnitOfWorkManager unitOfWorkManager, IMembershipService membershipService, 
            ILocalizationService localizationService, IRoleService roleService, ISettingsService settingsService) : 
            base(loggingService, unitOfWorkManager, membershipService, localizationService, roleService, settingsService)
        {
        }

        /// <summary>
        /// This is called when the forum is closed
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
