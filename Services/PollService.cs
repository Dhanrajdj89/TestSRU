using System;
using System.Collections.Generic;
using System.Linq;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Services.Data.Context;

namespace SportsRUsApp.Services
{
    public partial class PollService : IPollService
    {
        private readonly SportsRUsContext _context;
        public PollService(ISportsRUsContext context)
        {
            _context = context as SportsRUsContext;
        }

        public List<Poll> GetAllPolls()
        {
            return _context.Poll.ToList();
        }

        public Poll Add(Poll poll)
        {
            poll.DateCreated = DateTime.UtcNow;
            poll.IsClosed = false;
            return _context.Poll.Add(poll);
        }

        public Poll Get(Guid id)
        {
            return _context.Poll.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Poll item)
        {
            _context.Poll.Remove(item);
        }
    }
}
