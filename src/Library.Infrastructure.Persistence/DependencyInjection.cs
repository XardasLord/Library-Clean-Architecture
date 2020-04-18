﻿using System;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<LibraryDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("LibraryConnectionString"));
                })
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<ILibraryUserRepository, LibraryUserRepository>();

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