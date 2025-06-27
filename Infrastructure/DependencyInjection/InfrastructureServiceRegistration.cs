using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Services;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

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
            services.AddTransient<ITokenService, TokenService>();

            services.AddTransient<IShortUrlCodeGeneratorService, ShortUrlCodeGeneratorService>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
