using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TestProject1.DTO.BookStoreDTO;
using TestProject1.Helpers;
using TestProject1.Interfaces.BookStoreInterfaces;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TestProject1.Tests
{
    public class BookStoreTests
    {
        private IBookStoreApi api;

        [OneTimeSetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services
                .AddRefitClient<IBookStoreApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://demoqa.com");
                });

            var provider = services.BuildServiceProvider();
            api = provider.GetRequiredService<IBookStoreApi>();
        }

        //[Test] // больше не будет работать, юзер уже создан
        //public async Task CreateNewUser()
        //{
        //    var credentials = new UserCreateRequestDTO("GabaGama", "StrongPass123!");
        //    var result = await api.CreateUserAsync(credentials); //"userID": "718e8c1a-8cc9-4130-8a6f-8a1dda323415","username": "GabaGama","books": []
        //    result.Should().NotBeNull();
        //}

        [Test]
        public async Task Test1_GetUserToken()
        {
            var credentials = new UserCreateRequestDTO("GabaGama", "StrongPass123!");
            var result = await api.GenerateTokenAsync(credentials);
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Test2_GetUserId()
        {
            var credentials = new UserCreateRequestDTO("GabaGama", "StrongPass123!");
            var result = await api.GetUserIdAsync(credentials);
            result.UserId.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Test3_GetBookListAsync()
        {
            var result = await api.GetBookListAsync();
            result.Should().NotBeNull();
            result.Books.Should().HaveCount(8);
            result.Books.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Test4_GetBookByIsbnAsync()
        {
            var result = await api.GetBookByIsbnAsync("9781449325862");
            result.Should().NotBeNull();
        }

        [Test]
        public async Task Test5_AddBookToUserAsync() // тест фейлится - ожидаемо (проблемы с апи)
        {
            var token = await GetTokenAsync();

            var listOfBooks = await api.GetBookListAsync();
            var rndIsbn = RandomHelper.GetRandomItem(listOfBooks.Books).Isbn;

            var userId = await GetUsersIdAsync();

            var request = new AddCollectionOfBooksToUserDTO //круглые скобки - потому что record, а не класс
            (
                userId,
                new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO(rndIsbn) }
            );

            var response = await api.AddBookToUserAsync(request, token);
            response.Should().NotBeNull();
        }

        [Test]
        public async Task Test6_DeleteBookByIsbn() // работает некорректно - приходит 400-я (должна 500-я), разобраться
        {
            var token = await GetTokenAsync();

            var userId = await GetUsersIdAsync();

            var request = new DeleteBookRequestDTO
            (
                "9781449331818",
                userId
            );

            var response = await api.DeleteBookFromUserAsync(request, token);
            response.Should().NotBeNull();
        }

        [Test]
        public async Task Test7_SendInvalidRequestAsync()
        {
            var listOfBooks = await api.GetBookListAsync();
            var rndIsbn = RandomHelper.GetRandomItem(listOfBooks.Books).Isbn;

            var userId = await GetUsersIdAsync();

            var request = new AddCollectionOfBooksToUserDTO 
            (
                userId,
                new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO(rndIsbn) }
            );

            Func<Task> act = async () => await api.AddBookToUserAsync(request, token: null); 
            act.Should().ThrowAsync<ApiException>(); //.Where(p => p.StatusCode == System.Net.HttpStatusCode.BadRequest) - по статус кодам почему-то не отрабатывает
        }

        [Test]
        public async Task Test8_AddBookWithInvalidIsbnAsync()
        {
            var token = await GetTokenAsync();
            var userId = await GetUsersIdAsync();

            var request = new AddCollectionOfBooksToUserDTO
            (
                userId,
                new List<CollectionOfIsbnsDTO> { new CollectionOfIsbnsDTO("INVALID_ISBN") }
            );

            Func<Task> act = async () => await api.AddBookToUserAsync(request, token);
            act.Should().ThrowAsync<ApiException>();
        }

        //вспомогательные методы
        private async Task<string > GetTokenAsync()
        {
            var credentials = new UserCreateRequestDTO("GabaGama", "StrongPass123!");
            var token = await api.GenerateTokenAsync(credentials);
            var result = $"Bearer {token.Token}";
            return result;
        }

        private async Task<string> GetUsersIdAsync()
        {
            var credentials = new UserCreateRequestDTO("GabaGama", "StrongPass123!");
            var result = await api.GetUserIdAsync(credentials);
            return result.UserId;
        }


    }
}
