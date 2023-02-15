using Data.Entities;
using Data.Queries;

namespace Application.Interfaces.Repositories;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<Participant?> GetByQueryAsync(ParticipantQuery query);
}