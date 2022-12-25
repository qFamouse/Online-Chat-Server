using Application.Entities;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IParticipantRepository : IBaseRepository<Participant>
    {
        Task<Participant?> GetByQueryAsync(ParticipantQuery query);
    }
}
