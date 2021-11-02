using Estudos.Interfaces.Services;
using Estudos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Estudos.Helpers.DependencyInjection
{
    public static class Services
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services) 
        {
            services.AddTransient<IHttpClientService, HttpClientService>();
            
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<IPostsServiceQuery>(provider => 
                provider.GetRequiredService<IPostsService>());
            services.AddTransient<IPostsServiceCommand>(provider => 
                provider.GetRequiredService<IPostsService>());

            return services;
        } 
    }
}
