using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TestProject1.DTO;
using TestProject1.DTO.OrderDataDTOs;
using TestProject1.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;

namespace TestProject1.Tests
{
    public class OrderJsonTests
    {
        private OrderDTO order;

        [OneTimeSetUp]
        public void Setup()
        {
            order = FileReader.ReadJson<OrderDTO>("OrderData.json");
        }

        [Test]  //тест на проверку товаров - они есть и их 3
        public void Test1_CheckItemsIsNotNull()
        {
            foreach (var item in order.Items)
            {
                TestContext.WriteLine($"{item.ProductId} | {item.Quantity.ToString()} | {item.Price.ToString()}");
            }

            order.Items.Should().NotBeNull();
            order.Items.Should().HaveCount(3);
        }

        [Test] // подсчет стоимости всех товаров - соответствует тому, что в summary.itemsTotal

        public void Test2_CheckSumOfitems()
        {
            var sum = order.Items.Select(item => item.Quantity * item.Price).Sum();
            sum.Should().Be(order.Summary.ItemsTotal);
        }

        [Test] // в ордере есть 2 айтема категории "Electronics"

        public void Test3_CheckElectronicsQuantity()
        {
            var electronicsInOrder = order.Items.Where(item => item.Category == "Electronics").ToList();

            using (new AssertionScope())
            {
                electronicsInOrder.Should().OnlyContain(item => item.Category == "Electronics");
                electronicsInOrder.Should().HaveCount(2);
            }
        }
    }
}
