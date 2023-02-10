using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Online.Chat.Crone.Repositories.Abstractions;

namespace Online.Chat.Crone.Repositories.Implementations
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _context;

        public MessageRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task DeleteMessageById(int id)
        {
            var query = "DELETE FROM DirectMessages WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}
