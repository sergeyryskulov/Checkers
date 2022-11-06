using Checkers;
using System.Linq;
using Checkers.Web.Controllers;
using Checkers.Web.Controllers.api;
using Checkers.Web.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Checkers.IntegrationTests
{
    [TestClass()]
    public class StartupTests
    {
        [TestMethod]
        public void ConfigureServices_RegistersDependenciesCorrectly()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();

            IServiceCollection services = new ServiceCollection();

            var target = new Startup(configurationStub.Object);

            target.ConfigureServices(services);

            var configuration = target.Configuration;
            //  Mimic internal asp.net core logic.
            //services.AddTransient<HomeController>();


            foreach (var type in typeof(HomeController).Assembly.GetTypes().Where(t => t.Name.EndsWith("Controller") && !t.IsInterface))
            {
                services.AddTransient(type);
            }
            var serviceProvider = services.BuildServiceProvider();


            foreach (var type in typeof(HomeController).Assembly.GetTypes().Where(t => t.Name.EndsWith("Controller") && !t.IsInterface))
            {
                var controller = serviceProvider.GetService(type);
                Assert.IsNotNull(controller);
            }
        }

        [TestMethod]
        public void FunctionalStartupTest()
        {
            var client = new WebApplicationFactory<Startup>().CreateClient();
            var callResult = client.GetAsync("/Game").Result;
            var callResultString = callResult.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(callResultString.Contains("Игра"));
        }

        [TestMethod()]
        public void SwaggerConfigTest()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();

            var options = new Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions();

            var target = new Startup(configurationStub.Object);
            
            target.SwaggerConfig(options);

            Assert.IsNotNull(options);
            
        }

        [TestMethod()]
        public void GetSchemaName_WithAttributeTest()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();

            var target = new Startup(configurationStub.Object);

            var actual= target.GetSchemaName(typeof(BoardStateDto));

            Assert.AreEqual("BoardState", actual);

        }

        [TestMethod()]
        public void GetSchemaName_WithoutAttributeTest()
        {
            Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();

            var target = new Startup(configurationStub.Object);

            var actual = target.GetSchemaName(typeof(BoardController));

            Assert.AreEqual("BoardController", actual);

        }
    }
}