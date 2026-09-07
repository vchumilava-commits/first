using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject2.Interfaces;
using TestProject2.Services;
using Refit;

namespace TestProject2.Modules
{
    public static class UserModule
    {
        public static IServiceCollection AddUsersModule(this IServiceCollection services, string url)
        {
            services.AddRefitClient<IUserApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(url);
                });
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
