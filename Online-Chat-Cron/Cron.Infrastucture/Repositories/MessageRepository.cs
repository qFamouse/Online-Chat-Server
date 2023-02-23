using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces;
using Dapper;
using Shared;

namespace Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly DapperContext _context;

    public MessageRepository(DapperContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Message> GetByIdAsync(int id)
    {
        var query = "SELECT Id, SenderId, ReceiverId FROM DirectMessages WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var message = await connection.QuerySingleAsync<Message>(query, new { id });
            return message;
        }
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