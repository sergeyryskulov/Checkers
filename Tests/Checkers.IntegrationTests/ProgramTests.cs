using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Checkers.IntegrationTests
{
    [TestClass()]
    public class ProgramTests
    {

        [TestMethod()]
        public void CreateHostBuilderTest()
        {
            Program.CreateHostBuilder(new string[0]);
        }

        [TestMethod()]
        public void MainTest()
        {
            var hostBuilderStub = new Mock<IHostBuilder>();

            var host = new Mock<IHost>();

            hostBuilderStub.Setup(m => m.Build()).Returns(host.Object);

            var provider = new Mock<IServiceProvider>();

            var lifeTime = new Mock<IHostApplicationLifetime>();

            CancellationToken token = new CancellationToken(true);

            lifeTime.SetupGet(m => m.ApplicationStopping).Returns(token);

            provider.Setup(m => m.GetService(typeof(IHostApplicationLifetime))).Returns(lifeTime.Object);

            host.Setup(m => m.Services).Returns(provider.Object);

            Program.HostBuilderStub = hostBuilderStub.Object;

            Program.Main(new string[] { "/checkersunittest" });
        }
    }
}