﻿using System.Data;
using System.Data.SqlClient;

namespace OnlineChat.Cron
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IDbConnection CreateConnection() =>
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
