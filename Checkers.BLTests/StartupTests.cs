using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Text;
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

            var serviceProvider = services.BuildServiceProvider();

            var boardRepository = serviceProvider.GetService<IBoardRepository>();

            Assert.IsNotNull(boardRepository);
        }
    }
}