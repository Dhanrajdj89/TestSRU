using System;
using System.Linq;
using SportsRUsApp.Core.DataModel.Entities;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Services.Data.Context;

namespace SportsRUsApp.Services
{
    public partial class BlockService : IBlockService
    {
        private readonly SportsRUsContext _context;
        public BlockService(ISportsRUsContext context)
        {
            _context = context as SportsRUsContext;
        }

        public Block Add(Block block)
        {
            block.Date = DateTime.UtcNow;
            return _context.Block.Add(block);
        }

        public void Delete(Block block)
        {
            _context.Block.Remove(block);
        }

        public Block Get(Guid id)
        {
            return _context.Block.FirstOrDefault(x => x.Id == id);
        }
    }
}
