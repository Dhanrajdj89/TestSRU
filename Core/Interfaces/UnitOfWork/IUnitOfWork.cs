using System;
using System.Collections.Generic;
using SportsRUsApp.Core.Interfaces.Services;

namespace SportsRUsApp.Core.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Commit(List<string> cacheStartsWithToClear, ICacheService cacheService);
        void Rollback();
        void SaveChanges();
        void AutoDetectChangesEnabled(bool option);
        void LazyLoadingEnabled(bool option);
    }
}
