using System;
using HotChocolate.AspNetCore;
using Library.Domain.SharedKernel;
using Library.Infrastructure.Persistence.DbContexts;
using Library.Infrastructure.Persistence.GraphQL.Queries;
using Library.Infrastructure.Persistence.GraphQL.Types;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        private const string ConnectionStringConfigName = "LibraryConnectionString";

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<LibraryDbContext>(options =>
                {
                    options.EnableDetailedErrors();
                    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringConfigName));
                })
                .AddScoped(typeof(IAggregateRepository<>), typeof(AggregateRepository<>))
                .AddScoped(typeof(IAggregateReadRepository<>), typeof(AggregateRepository<>));

        public static IServiceCollection AddGraphQlQueries(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<BookViewModelQueries>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddType<BookViewModelType>();

            // services.AddErrorFilter<GraphQLErrorFilter>();

            return services;
        }

        public static IApplicationBuilder UseGraphQlQueries(
            this IApplicationBuilder app,
            IConfiguration graphQlConfiguration,
            IWebHostEnvironment env)
        {
            var graphQlEndpoint = graphQlConfiguration.GetSection("EndpointUrl").Value;

            return app.UseEndpoints(x => x.MapGraphQL(graphQlEndpoint)
                .WithOptions(new GraphQLServerOptions
                {
                    Tool =
                    {
                        Enable = env.IsDevelopment()
                    }
                }));
        }

        public static IHost MigrateDatabase(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<LibraryDbContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Apply logger here
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}