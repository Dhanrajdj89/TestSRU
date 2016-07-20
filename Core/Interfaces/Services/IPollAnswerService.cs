using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IPollAnswerService
    {
        List<PollAnswer> GetAllPollAnswers();
        PollAnswer Add(PollAnswer pollAnswer);
        List<PollAnswer> GetAllPollAnswersByPoll(Poll poll);
        PollAnswer Get(Guid id);
        void Delete(PollAnswer pollAnswer);
    }
}
