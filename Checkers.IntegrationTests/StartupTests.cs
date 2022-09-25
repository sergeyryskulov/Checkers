using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.BL.Services;
using Checkers.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Checkers.Tests
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
    }
}