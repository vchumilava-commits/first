using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.DependencyInjection;
using TestProject1.Interfaces;
using TestProject1.Modules;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TestProject1.Preconditions
{
    public class DependencyInjectionPreconditions
    {
        public ServiceProvider Provider { get; }

        public DependencyInjectionPreconditions()
        {
            var services = new ServiceCollection();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<UserNotifier>();

            Provider = services.BuildServiceProvider();
        }
    }
}
