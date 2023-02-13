﻿using Dapper;
using OnlineChat.Cron.Repositories.Abstractions;

namespace OnlineChat.Cron.Repositories.Implementations
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _context;

        public MessageRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task DeleteMessageByIdAsync(int id)
        {
            var query = "DELETE FROM DirectMessages WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }
    }
}