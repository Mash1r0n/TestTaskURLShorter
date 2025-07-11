﻿using Application.UseCases.Authorization.Login;
using Application.UseCases.Authorization.Registration;
using Application.UseCases.ShortUrls.CreateShortUrl;
using Application.UseCases.ShortUrls.RegisterClickAndReturnLongUrl;
using Application.UseCases.ShortUrls.RetrieveShortUrlInfo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<CreateShortUrlHandler>();
            services.AddTransient<RetrieveShortUrlInfoHandler>();
            services.AddTransient<RegistrationHandler>();
            services.AddTransient<RegisterClickAndReturnLongUrlHandler>();
            services.AddTransient<LoginHandler>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}