using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var infrastructureAssembly = AppDomain.CurrentDomain.Load("Library.Infrastructure.Persistence");
            
            return services
                .AddMediatR(Assembly.GetExecutingAssembly(), infrastructureAssembly)
                .AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
