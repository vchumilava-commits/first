using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tests1.DependencyInjectionTask;

namespace Tests1.Modules
{
    public static class NotificationModule
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddScoped<EmailSender>();
            services.AddScoped<UserNotifier>();
            return services;
        }
    }
}
