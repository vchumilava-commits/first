using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject2.Modules;

namespace TestProject2.Fixtures
{
    public class TestPreconditions
    {
        public IServiceProvider Provider { get; }

        public TestPreconditions()
        {
            var services = new ServiceCollection();
            services.AddUsersModule("https:/123.com");

            Provider = services.BuildServiceProvider();
        }
    }
}
