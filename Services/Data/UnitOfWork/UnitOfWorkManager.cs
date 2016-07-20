using System.Data.Entity;
using SportsRUsApp.Core.Constants;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.UnitOfWork;
using SportsRUsApp.Services.Data.Context;
using SportsRUsApp.Services.Migrations;

namespace SportsRUsApp.Services.Data.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private bool _isDisposed;
        private readonly SportsRUsContext _context;

        public UnitOfWorkManager(ISportsRUsContext context)
        {
            //http://www.entityframeworktutorial.net/code-first/automated-migration-in-code-first.aspx
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SportsRUsContext, Configuration>(SiteConstants.Instance.SportsRUsContext));
            _context = context as SportsRUsContext;
        }

        /// <summary>
        /// Provides an instance of a unit of work. This wrapping in the manager
        /// class helps keep concerns separated
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(_context);
        }

        /// <summary>
        /// Make sure there are no open sessions.
        /// In the web app this will be called when the injected UnitOfWork manager
        /// is disposed at the end of a request.
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _context.Dispose();
                _isDisposed = true;
            }
        }
    }
}
