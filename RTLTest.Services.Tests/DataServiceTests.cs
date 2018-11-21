using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RTLTest.Services.AutoMapper;
using RTLTestTask.Db;
using RTLTestTask.Models;
using Xunit;

namespace RTLTest.Services.Tests
{
    public class DataServiceTests
    {
        private IServiceProvider _provider;
        public DataServiceTests()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RTLDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            services.AddTransient<ModelMapper>();
            services.AddTransient<IDataService, DataService>();

            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task GetShowTest()
        {
            // Arrange 
            var db = _provider.GetService<RTLDbContext>();
            PrepareData(db);

            var srv = _provider.GetService<IDataService>();

            // Act
            var data = await srv.GetShow(1);

            //Assert
            data.Should().NotBeNull();
            data.Id.Should().Be(1);
        }

        [Fact]
        public async Task GetShowListTest()
        {
            // Arrange 
            var db = _provider.GetService<RTLDbContext>();
            PrepareData(db);

            var srv = _provider.GetService<IDataService>();

            // Act
            var dataAll = await srv.GetShowList(1, 20);
            var data = await srv.GetShowList(2,2);

            //Assert
            data.Should().NotBeNull();
            dataAll.Should().NotBeNull();
            dataAll.Should().HaveCount(3);
            data.Should().HaveCount(1);
        }

        private void PrepareData(RTLDbContext db)
        {
            var show1 = new TVShow()
            {
                Id = 1,
                Name = "Test Show",
                ShowCasts = new List<ShowCast>()
                {
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "John Doe",
                            Id = 1,
                            DoB = new DateTime(1990, 01, 08)
                        }
                    },
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "Chris Redfield",
                            Id = 2,
                            DoB = new DateTime(1978, 06, 12)
                        }
                    }
                }
            };

            var show2 = new TVShow()
            {
                Id = 2,
                Name = "Test Show 2",
                ShowCasts = new List<ShowCast>()
                {
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "John Smith",
                            Id = 3,
                            DoB = new DateTime(1980, 08, 08)
                        }
                    },
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "Jill Valentine",
                            Id = 4,
                            DoB = new DateTime(1977, 07, 17)
                        }
                    }
                    ,
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "Chris Redfield",
                            Id = 2,
                            DoB = new DateTime(1978, 06, 12)
                        }
                    }
                }
            };

            var show3 = new TVShow()
            {
                Id = 3,
                Name = "Test Show 3",
                ShowCasts = new List<ShowCast>()
                {
                    new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = "Charlie Chaplin",
                            Id = 5,
                            DoB = new DateTime(1920, 01, 08)
                        }
                    }
                }
            };

            db.TVShows.Add(show1);
            db.TVShows.Add(show2);
            db.TVShows.Add(show3);
            db.SaveChanges();

        }
    }
}
