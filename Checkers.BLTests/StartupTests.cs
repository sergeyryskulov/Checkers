using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.BL.Services;
using Checkers.Controllers;
using Ckeckers.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
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
    }
}