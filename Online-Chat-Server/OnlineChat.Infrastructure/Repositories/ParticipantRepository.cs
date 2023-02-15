using Application.Interfaces.Repositories;
using Application.Queries;
using Microsoft.EntityFrameworkCore;
using Shared;
using Data.Entities;

namespace Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(OnlineChatContext context) : base(context) { }

        public async Task<Participant?> GetByQueryAsync(ParticipantQuery query)
        {
            return await GetByQuery(query).FirstOrDefaultAsync();
        }

        private IQueryable<Participant> GetByQuery(ParticipantQuery query)
        {
            IQueryable<Participant> participantsQuery = DbContext.Participants;

            if (query.UserId.HasValue)
            {
                participantsQuery = participantsQuery.Where(x => x.UserId == query.UserId);
            }

            if (query.ConversationId.HasValue)
            {
                participantsQuery = participantsQuery.Where(x => x.ConversationId == query.ConversationId);
            }

            return participantsQuery;
        }
    }
}
