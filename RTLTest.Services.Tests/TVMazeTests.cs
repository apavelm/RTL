using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Xunit;

namespace RTLTest.Services.Tests
{
    public class TVMazeTests
    {
        private IServiceProvider _provider;
        public TVMazeTests()
        {
            var services = new ServiceCollection();

            services.AddRefitClient<ITVMazeClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api.tvmaze.com"));

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task ITVMazeClientTest()
        {
            // Arrange 
            var srv = _provider.GetService<ITVMazeClient>();

            // Act
            var data = await srv.GetTheShowWithActors(1, new CancellationToken());

            //Assert
            data.Should().NotBeNull();
            data.Id.Should().Be(1);
        }
    }
}
