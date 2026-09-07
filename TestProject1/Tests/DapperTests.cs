using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;
using TestProject1.Preconditions;
using TestProject1.Interfaces.DapperTestsInterfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using TestProject1.DTO.DapperTetstDTO;

namespace TestProject1.Tests
{
    public class DapperTests
    {
        private readonly DataBasePreconditions p = new DataBasePreconditions();

        [Test]
        public async Task Test1_CheckAllUsers()
        {
            var repo= p.Provider.GetService<IUserRepository>();
            var users = await repo.GetUsersAsync();
            users.Should().HaveCount(15);
        }

        [Test]
        public async Task Test2_GetUserById()
        {
            var repo = p.Provider.GetService<IUserRepository>();
            var users = await repo.GetUserByIdAsync(15);
            users.Should().NotBeNull();
        }

        [Test]
        public async Task Test3_GetUserByNameAndSurname()
        {
            var repo = p.Provider.GetService<IUserRepository>();
            var users = await repo.GetUserByNameAndSurname("Мария", "Павлова");
            users.Should().NotBeNull();
            users.firstName.Should().Be("Мария");
            users.lastName.Should().Be("Павлова");
        }

        [Test]
        public async Task Test4_GetAddressByUserId()
        {
            var repo = p.Provider.GetService<IAddressRepository>();
            var address = await repo.GetAddressByUserId(1);
            address.Should().NotBeNull();
        }

        [Test] 
        public async Task Test5_GetCountCategoriesFromDb()
        {
            var repo = p.Provider.GetService<ICategoryRepository>();
            var categories = await repo.GetCategories();
            categories.Should().HaveCount(6);
        }

        [Test]
        public async Task Test6_GetProductById()
        {
            var repo = p.Provider.GetService<IProductRepository>();
            var product = await repo.GetProductById(2);
            product.name.Should().Be("Samsung Galaxy S24");
            product.description.Should().Be("Флагманский смартфон Samsung");
            product.price.Should().Be(69990);
            product.stock.Should().Be(20);
            product.categoryId.Should().Be(1);
        }

        [Test] 
        public async Task Test7_GetOrderByIdAndCheckItemsInThisOrder()
        {
            var orderRepo = p.Provider.GetService<IOrderRepository>();
            var itemsRepo = p.Provider.GetService<IOrderItemsRepository>();

            var order = await orderRepo.GetOrderByUserId(1, 1);
            order.Should().NotBeNull();

            var items = await itemsRepo.GetOrderItemsByOrderId((int)order.id);
            items.Should().HaveCount(2);

            var productIds = items.Select(item => item.productId).ToList();
            productIds.Should().BeEquivalentTo(new[] { 1L, 15L });
        }

        [Test]
        public async Task Test8_AccessoriesBuyersFromDifferentCities()
        {
            var productRepo = p.Provider.GetService<IProductRepository>();
            var itemsRepo = p.Provider.GetService<IOrderItemsRepository>();
            var orderRepo = p.Provider.GetService<IOrderRepository>();
            var addressRepo = p.Provider.GetService<IAddressRepository>();

            var accessoryProductsIds = (await productRepo.GetProductsByCategoryId(6))
                .Select(product => product.id)
                .ToList();

            var orderIds = (await itemsRepo.GetOrderItemsByProductIds(accessoryProductsIds))
                .Select(item => item.orderId)
                .Distinct()
                .ToList();

            var userIds = (await orderRepo.GetOrdersByIds(orderIds))
                .Select(order => order.userId)
                .Distinct()
                .ToList();

            var cities = new List<string>();
            foreach (var userId in userIds)
            {
                var address = await addressRepo.GetAddressByUserId((int)userId);
                cities.Add(address.city);
            }
            cities.Should().Contain(new[] { "Москва", "Екатеринбург" });
        }

        [Test] 
        public async Task Test9_BuyersTVBuyAccessories()
        {
            var productRepo = p.Provider.GetService<IProductRepository>();
            var itemsRepo = p.Provider.GetService<IOrderItemsRepository>();
            var orderRepo = p.Provider.GetService<IOrderRepository>();

            var tvProductsIds = (await productRepo.GetProductsByCategoryId(4))
                .Select(product => product.id)
                .ToList();

            var tvOrderIds = (await itemsRepo.GetOrderItemsByProductIds(tvProductsIds))
                .Select(item => item.orderId)
                .Distinct()
                .ToList();

            var tvBuyerIds = (await orderRepo.GetOrdersByIds(tvOrderIds))
                .Select(order => order.userId)
                .Distinct()
                .ToList();

            var accessoryProductsIds = (await productRepo.GetProductsByCategoryId(6))
                .Select(product => product.id)
                .ToList();

            var accessoryOrderIds = (await itemsRepo.GetOrderItemsByProductIds(accessoryProductsIds))
                .Select(item => item.orderId)
                .Distinct()
                .ToList();

            var accessoryBuyerIds = (await orderRepo.GetOrdersByIds(accessoryOrderIds))
                .Select(order => order.userId)
                .Distinct()
                .ToList();

            tvBuyerIds.Should().BeSubsetOf(accessoryBuyerIds);
        }



        //  [Test]
        //    public async Task InitialiseTest()
        //   {
        //        var connectionString = "Data Source=marketplace.db";
        //
        //      await using var connection = new SqliteConnection(connectionString);

        //      await connection.OpenAsync();
        //
        //       await DatabaseInitializer.InitializeAsync(connection);
        //  }
    }
}
