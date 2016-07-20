using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IVoteService
    {
        Vote Add(Vote vote);
        Vote Get(Guid id);
        void Delete(Vote vote);
        IList<Vote> GetAllVotesByUser(Guid membershipId);
        List<Vote> GetVotesByPosts(List<Guid> postIds);
        List<Vote> GetVotesByPost(Guid postId);
    }
}
