using System;
using SportsRUsApp.Core.DataModel.Entities;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IBlockService
    {
        Block Add(Block block);        
        void Delete(Block block);
        Block Get(Guid id);
    }
}
