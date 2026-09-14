using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.Playwright;

namespace TestProject1.Tests.UITests
{
    public class HerokuTests : BaseTest
    {
        [Test]
        public async Task CheckBoxTest()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
            var first = Page.Locator("input[type='checkbox']").Nth(0);
            await first.CheckAsync();
            (await first.IsCheckedAsync()).Should().BeTrue();
        }

        [Test]
        public async Task FormAuthentification_AuthoriseWithWrongPasswordAsync()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/login");

            var usernameTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Username" }); //playwright локатор
            await usernameTextBox.FillAsync("nottomsmith");

            //var passTextBox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
            //var passwordTextBox = Page.Locator("#password"); //css локатор вариация 1
            var passwordTextBox = await Page.QuerySelectorAsync("#password"); //ccs локатор вариация 2
            await passwordTextBox.FillAsync("SuperSecretPassword!");

            var loginButton = Page.GetByRole(AriaRole.Button, new() { Name = "Login" }); //playwright локатор
            await loginButton.ClickAsync();

            var errorMessageLabel = Page.Locator("//div[@id='flash']"); //x-path локатор
            var errorMessageText = await errorMessageLabel.TextContentAsync();

            errorMessageText.Should().Contain("Your username is invalid!");
        }
    }
}