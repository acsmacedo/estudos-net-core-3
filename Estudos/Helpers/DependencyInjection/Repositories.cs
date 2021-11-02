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
                services.AddTransient<IMySqlDatabaseClient, MySqlDatabaseClient>();
                
                services.AddTransient<IPostsRepository, PostsMySqlRepository>();
                services.AddTransient<IPostsRepositoryQuery>(provider => 
                    provider.GetRequiredService<IPostsRepository>());
                services.AddTransient<IPostsRepositoryCommand>(provider => 
                    provider.GetRequiredService<IPostsRepository>());
            }

            return services;
        } 
    }
}
