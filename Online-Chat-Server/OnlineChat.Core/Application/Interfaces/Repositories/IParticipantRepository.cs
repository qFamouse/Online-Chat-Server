using Application.Queries;
using Data.Entities;

namespace Application.Interfaces.Repositories;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    Task<Participant?> GetByQueryAsync(ParticipantQuery query);
}