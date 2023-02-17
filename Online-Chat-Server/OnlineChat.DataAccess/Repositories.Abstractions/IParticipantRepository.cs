using Domain.Entities;
using Domain.Queries;

namespace Repositories.Abstractions;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<Participant?> GetByQueryAsync(ParticipantQuery query);
}