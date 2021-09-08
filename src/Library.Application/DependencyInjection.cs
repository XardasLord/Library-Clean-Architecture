using System.Reflection;
using Library.Application.Configurations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddApplicationConfiguration(configuration);

        private static IServiceCollection AddApplicationConfiguration(this IServiceCollection service, IConfiguration configuration)
            => service.Configure<StorageConfig>(configuration.GetSection("StorageConfig"));
    }
}
