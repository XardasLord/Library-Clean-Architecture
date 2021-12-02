using System;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using Library.Infrastructure.Persistence.DbContexts;
using Library.Infrastructure.Persistence.GraphQL.Queries;
using Library.Infrastructure.Persistence.GraphQL.Types;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
                //.AddDbContext<LibraryViewDbContext>(options =>
                //{
                //    options.EnableDetailedErrors();
                //    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringConfigName));
                //})
                .AddScoped<IAggregateRepository<LibraryUser>, AggregateRepository<LibraryUser>>()
                .AddScoped<IAggregateRepository<Book>, AggregateRepository<Book>>();

        public static IServiceCollection AddGraphQLQueries(this IServiceCollection services) 
            => services
                .AddGraphQL(
                    SchemaBuilder.New()
                        .AddQueryType<StorageViewModelQuery>()
                        .AddType<BookViewModelType>()
                        .AddType<StorageViewModelType>()
                        .Create(),
                    new QueryExecutionOptions
                    {
                        ForceSerialExecution = true
                    });

        public static IApplicationBuilder UseGraphQLQueries(this IApplicationBuilder app, IConfiguration graphQLConfiguration)
            => app
                .UseGraphQL(new PathString(graphQLConfiguration.GetSection("EndpointUrl").Value))
                .UsePlayground(new PathString(graphQLConfiguration.GetSection("EndpointUrl").Value));

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
