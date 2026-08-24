using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TestProject1.DTO;
using FluentAssertions;
using FluentAssertions.Execution;
using TestProject1.DTO.UserDataDTO;

namespace TestProject1.AutoTests;


public class UserJsonTests
{
    private DTO.UserDataDTO.UserRootDTO userdata;

    [OneTimeSetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string json = File.ReadAllText(path);

        userdata = JsonSerializer.Deserialize<UserRootDTO>(json);
    }

    [Test]
    public void Test1_CheckUserDataCountIs10()
    {
        foreach (var data in userdata.Data)
        {
            TestContext.WriteLine($"Result\n{data.Id}");
        }
        userdata.Data.Should().HaveCount(10, "в файле должно быть ровно 10 пользователей"); // 10 элементов
    }

    [Test]
    public void Test2_CheckFirstUserisAliceJohnson()
    {
        userdata.Data.FirstOrDefault()?.UserProfile?.FullName
           .Should().Be("Alice Johnson", "первым пользователем в списке должна быть Alice Johnson");
    }

    [Test]
    public void Test3_allIDsShouldHaveUniqueItems()
    {
        List<int> allIds = userdata.Data.Select(u => u.Id).ToList();
        allIds.Should().OnlyHaveUniqueItems("все идентификаторы пользователей (Id) должны быть уникальными");
    }

    [Test]
    public void Test4_CheckAtListOnePremium()
    {
        userdata.Data.Should().Contain(u => u.UserProfile.Tags.Contains("premium"),
        "в списке должен быть как минимум один пользователь с тегом 'premium'");
    }

    [Test]
    public void Test5_CheckCityisNotEmpty()
    {
        userdata.Data.Should().AllSatisfy(u =>
                u.UserProfile?.UserAddress?.City.Should().NotBeNullOrWhiteSpace("город у каждого пользователя должен быть заполнен"));
    }

    [Test]
    public void Test6_CheckStockholmIsThere()
    {
        userdata.Data.Should().Contain(u => u.UserProfile != null && u.UserProfile.UserAddress != null &&
                                            u.UserProfile.UserAddress.City.Equals("Stockholm", StringComparison.OrdinalIgnoreCase),
                "в списке должен быть хотя бы один житель Стокгольма");
    }

    [Test]
    public void Test7_CheckAllUsersAreAdults()
    {
        userdata.Data.Should().AllSatisfy(u =>
        u.UserProfile?.Age.Should().BeInRange(18, 60, "возраст всех пользователей должен быть от 18 до 60 лет"));
    }

    [Test]
    public void Test8_CheckAdminIsThere()
    {
        userdata.Data.Should().Contain(u => u.Roles != null && u.Roles.Contains("admin"),
        "в списке должен быть хотя бы один пользователь с ролью 'admin'");
    }
}