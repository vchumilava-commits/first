using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.Modules;

namespace TestProject1.Preconditions
{
    public class DataBasePreconditions
    {
        public ServiceProvider Provider { get; }
        public DataBasePreconditions()
        {
            var services = new ServiceCollection();
            services.AddDataAccessMarketplace("Data Source=marketplace.db");
            Provider = services.BuildServiceProvider();
        }
    }
}
