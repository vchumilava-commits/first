using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.Playwright;

namespace TestProject1.Tests.UITests
{
    public class SauceDemoTests : BaseTest
    {
        [Test]
        public async Task SuccessfulLogin_UserMovedToProductsPage()
        {
            await Page.GotoAsync("https://www.saucedemo.com/");

            var usernameTextBox = Page.Locator("//input[@id='user-name']");
            await usernameTextBox.FillAsync("standard_user");

            var passwordTextBox = Page.Locator("//input[@id='password']");
            await passwordTextBox.FillAsync("secret_sauce");

            var loginButton = Page.Locator("//input[@id='login-button']");
            await loginButton.ClickAsync();

            var productsTitle = Page.Locator("//span[text()='Products']");
            (await productsTitle.IsVisibleAsync()).Should().BeTrue();
        }
    }
}
