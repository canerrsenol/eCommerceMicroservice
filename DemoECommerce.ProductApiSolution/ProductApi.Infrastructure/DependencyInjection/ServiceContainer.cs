using eCommerce.SharedLibrary.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services
            , IConfiguration config)
        {
            // Add database connectivity 
            // Add authentication sceme
            SharedServiceContainer.AddSharedServices<ProductDBContext>(services, config, config["MySerilog:FineName"]!);

            // Create Dependency Injection (DI)
            

            return services;
        }
    }
}
