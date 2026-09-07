using System.Text.Json;
using System.Net.Http.Json;
using System.Net;

using TestProject1.DTO;

namespace TestProject1.Tests;

public class UnitTests
{
    private static HttpClient client;

    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3I0eNf3AceBcmA9MKgqcHXuLofR");
    }

    [Test]
    public async Task Test1_StatusCodeCheck()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2_FirstDTOCheck()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string jsonGet = await response.Content.ReadAsStringAsync();
        UserResponseDTO userResponse = JsonSerializer.Deserialize<UserResponseDTO>(jsonGet);
        UserDataDTO user = userResponse.Data;
    }

    // домашка отсюда
    [Test]
    public async Task Test3_CreateRequestTest() // тест на возможность создания и на то, что возвращается
    {
        var newUser = new CreateUserRequestDTO { Name = "Jack", Job = "Evernode" };

        using HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created)); //201 проверяем

        var createdUser = await response.Content.ReadFromJsonAsync<CreateUserResponseDTO>();

        //собственно проверяем, что данные засейвились
        Assert.Multiple(() => {
            Assert.That(createdUser.Name, Is.EqualTo("Jack"));
            Assert.That(createdUser.Job, Is.EqualTo("Evernode"));
            Assert.That(createdUser.Id, Is.Not.Null);
            Assert.That(createdUser.CreatedAt, Is.Not.Null);

        });
    }

    [Test]
    public async Task Test4_PutRequestTest() // put-запрос
    {

        var updatedUser = new CreateUserRequestDTO { Name = "Jack", Job = "Evernode1" }; //компания отличается от теста 3

        using HttpResponseMessage response = await client.PutAsJsonAsync("users/2", updatedUser);

        response.EnsureSuccessStatusCode(); //по заданию сказано проверить как в тесте 1
    }

    [Test]
    public async Task Test5_DeleteRequestTest() // delete-запрос
    {
        using HttpResponseMessage response = await client.DeleteAsync("users/2");
        response.EnsureSuccessStatusCode();
    }


    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}