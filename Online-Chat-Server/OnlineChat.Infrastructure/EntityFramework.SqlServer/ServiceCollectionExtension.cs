using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace EntityFramework.SqlServer
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
