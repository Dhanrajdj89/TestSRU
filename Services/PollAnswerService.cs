using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Services.Data.Context;
using SportsRUsApp.Utilities;

namespace SportsRUsApp.Services
{
    public partial class PollAnswerService : IPollAnswerService
    {
        private readonly SportsRUsContext _context;
        public PollAnswerService(ISportsRUsContext context)
        {
            _context = context as SportsRUsContext;
        }

        public List<PollAnswer> GetAllPollAnswers()
        {
            return _context.PollAnswer
                    .Include(x => x.Poll).ToList();
        }

        public PollAnswer Add(PollAnswer pollAnswer)
        {
            pollAnswer.Answer = StringUtils.SafePlainText(pollAnswer.Answer);
            return _context.PollAnswer.Add(pollAnswer);
        }

        public List<PollAnswer> GetAllPollAnswersByPoll(Poll poll)
        {
            var answers = _context.PollAnswer
                    .Include(x => x.Poll)
                    .AsNoTracking()
                    .Where(x => x.Poll.Id == poll.Id).ToList();
            return answers;
        }

        public PollAnswer Get(Guid id)
        {
            return _context.PollAnswer.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(PollAnswer pollAnswer)
        {
            _context.PollAnswer.Remove(pollAnswer);
        }

    }
}
