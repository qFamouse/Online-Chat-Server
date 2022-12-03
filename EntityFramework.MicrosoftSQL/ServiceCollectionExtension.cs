using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.MicrosoftSQL
{
    public static class ServiceCollectionExtension
    {
        public static void AddSqlServerDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OnlineChatContext>(
                options => options.UseSqlServer(
                    connectionString, x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
        }
    }
}
