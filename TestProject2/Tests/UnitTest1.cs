using TestProject2.Fixtures;
using TestProject2.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject2.Tests
{
    public class Tests
    {
        private readonly TestPreconditions p= new();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var users = p.Provider.GetService<IUserService>();
            var user = users.GetUser(1);
            Assert.Pass();
            Assert.Pass();
        }
    }
}
