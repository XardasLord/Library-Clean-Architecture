using Library.Infrastructure.Authorization;
using Library.Infrastructure.ErrorHandling;
using Library.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDatabase(configuration)
                .AddGraphQLQueries()
                .AddTokenAuthentication(configuration);

        public static IApplicationBuilder UseInfrastructure(
            this IApplicationBuilder app,
            IConfiguration configuration,
            IWebHostEnvironment env)
            => app
                .UseHttpsRedirection()
                .UseRouting()
                .UseMiddleware<ErrorHandlingMiddleware>()
                .UseTokenAuthentication()
                .UseTokenAuthorization()
                .UseGraphQLQueries(configuration.GetSection("Infrastructure:GraphQL"), env);
    }
}