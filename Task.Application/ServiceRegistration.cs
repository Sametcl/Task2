using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces.UnitOfWork;

namespace Task.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services,IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["CacheSettings:ConnectionString"];
                options.InstanceName = "BackendTask_";
            });
        }
    }
}
