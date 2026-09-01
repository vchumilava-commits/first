using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.DependencyInjection;
using TestProject1.Interfaces;

namespace TestProject1.Modules
{
    public static class DependencyInjectionModule
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender>();
            services.AddScoped<UserNotifier>();
            return services;
        }
    }
}
