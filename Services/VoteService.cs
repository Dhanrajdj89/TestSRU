﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.Events;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Services.Data.Context;

namespace SportsRUsApp.Services
{
    public partial class VoteService : IVoteService
    {
        private readonly IMembershipUserPointsService _membershipUserPointsService;
        private readonly SportsRUsContext _context;

        public VoteService(ISportsRUsContext context, IMembershipUserPointsService membershipUserPointsService)
        {
            _membershipUserPointsService = membershipUserPointsService;
            _context = context as SportsRUsContext;
        }

        public Vote Get(Guid id)
        {
            return _context.Vote.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Vote vote)
        {
            // Delete any points associated with this vote
            _membershipUserPointsService.Delete(PointsFor.Vote, vote.Id);

            // Delete the vote
            _context.Vote.Remove(vote);
        }

        public IList<Vote> GetAllVotesByUser(Guid membershipId)
        {
            return _context.Vote.Where(x => x.VotedByMembershipUser.Id == membershipId).ToList();
        }

        public List<Vote> GetVotesByPosts(List<Guid> postIds)
        {
            return _context.Vote
                        .Include(x => x.VotedByMembershipUser)
                        .Include(x => x.User)
                        .Include(x => x.Post)
                        .AsNoTracking()
                        .Where(x => postIds.Contains(x.Post.Id)).ToList();
        }

        public List<Vote> GetVotesByPost(Guid postId)
        {
            return _context.Vote
            .Include(x => x.VotedByMembershipUser)
            .Include(x => x.User)
            .Include(x => x.Post)
            .Where(x => x.Post.Id == postId).ToList();
        }

        /// <summary>
        /// Add a new vote
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        public Vote Add(Vote vote)
        {

            var e = new VoteEventArgs {Vote = vote};
            EventManager.Instance.FireBeforeVoteMade(this, e);

            if (!e.Cancel)
            {
                _context.Vote.Add(vote);

                EventManager.Instance.FireAfterVoteMade(this, new VoteEventArgs {Vote = vote});
            }

            return vote;
        }



    }
}
