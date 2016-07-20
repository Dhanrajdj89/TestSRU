using System;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.UnitOfWork
{
    public partial interface IUnitOfWorkManager : IDisposable
    {
        //IUnitOfWork NewUnitOfWork(bool isReadyOnly);     
        IUnitOfWork NewUnitOfWork();
    }
}
