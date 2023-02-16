using Data.Entities;
using Data.Queries;

namespace Repositories.Abstractions;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<Participant?> GetByQueryAsync(ParticipantQuery query);
}