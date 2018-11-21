using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RTLTest.Services.AutoMapper;
using Xunit;

namespace RTLTest.Services.Tests
{
    public class ScrapperTests
    {
        private IServiceProvider _provider;
        public ScrapperTests()
        {
            var services = new ServiceCollection();

            services.AddRefitClient<ITVMazeClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://api.tvmaze.com"));

            services.AddTransient<ModelMapper>();

            services.AddTransient<IScrapperService, ScrapperService>();

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task ScrapperServiceTest()
        {
            // Arrange 
            var srv = _provider.GetService<IScrapperService>();

            // Act
            var data = await srv.ScrapTheShow(1, new CancellationToken());

            //Assert
            data.Should().NotBeNull();
            data.Id.Should().Be(1);
        }

        [Fact]
        public async Task ScrapPageTest()
        {
            // Arrange 
            var srv = _provider.GetService<IScrapperService>();

            // Act
            var data = await srv.ScrapPage(1, new CancellationToken());

            //Assert
            data.Should().NotBeNull();
            data.Should().HaveCountGreaterThan(100);
        }
    }
}
