﻿using Library.Domain.AggregateModels.BookAggregate;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => AddDatabase(services, configuration);

        private static IServiceCollection AddDatabase(IServiceCollection services, IConfiguration configuration)
            => services
                .AddDbContext<LibraryDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("LibraryConnectionString"));
                })
                .AddScoped<IBookRepository, BookRepository>();
    }
}