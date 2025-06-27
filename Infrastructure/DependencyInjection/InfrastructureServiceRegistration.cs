using Application.Interfaces;
using Infrastructure.ShortCodeGenerator;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                _ = opts.UseSqlServer(connectionString);
            });

            services.AddTransient<IShortUrlRepository, ShortUrlRepository>();
            services.AddTransient<IUrlDynamicMetadataRepository, UrlDynamicMetadataRepository>();

            services.AddTransient<ICodeGenerator, CodeGenerator>();

            return services;
        }
    }
}
