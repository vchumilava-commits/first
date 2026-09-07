using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.Interfaces.DapperTestsInterfaces;
using TestProject1.Repositories;

namespace TestProject1.Modules
{
    public static class DataAccessMarketplaceModule
    {
        public static IServiceCollection AddDataAccessMarketplace(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserRepository>(p => new UserRepository(connectionString));
            services.AddScoped<IAddressRepository>(p => new AddressRepository(connectionString));
            services.AddScoped<ICategoryRepository>(p => new CategoryRepository(connectionString));
            services.AddScoped<IProductRepository>(p => new ProductRepository(connectionString));
            services.AddScoped<IOrderRepository>(p => new OrderRepository(connectionString));
            services.AddScoped<IOrderItemsRepository>(p => new OrderItemsRepository(connectionString));
            return services;
        }
    }
}
