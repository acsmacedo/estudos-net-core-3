using Estudos.DAL;
using Estudos.Interfaces.DAL;
using Estudos.Interfaces.Repositories;
using Estudos.Repositories.MySql;
using Microsoft.Extensions.DependencyInjection;

namespace Estudos.Helpers.DependencyInjection
{
    public static class Repositories 
    { 
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            string databaseName) 
        {
            if (databaseName == "MySql")
            {
                services.AddSingleton<IMySqlDatabaseClient, MySqlDatabaseClient>();
                services.AddSingleton<IPostsRepository, PostsMySqlRepository>();
            }

            return services;
        } 
    }
}
