using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RTLTestTask.Models;
using Xunit;

namespace RTLTestTask.Db.Tests
{
    public class DbTests
    {
        [Fact]
        public void Create()
        {
            // Arrange
            var show = new TVShow()
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

            var db = new RTLDbContext(new DbContextOptionsBuilder<RTLDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            // Act
            db.TVShows.Add(show);
            db.SaveChanges();

            // Assert
            var showGet = db.TVShows.FirstOrDefault(e => e.Id == 1);
            showGet.Should().NotBeNull();
            showGet.Name.Should().Be(show.Name);

            var actors = db.Actors.ToArray();
            actors.Should().HaveCount(2);
        }

        [Fact]
        public void Create2()
        {
            // Arrange
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

            var db = new RTLDbContext(new DbContextOptionsBuilder<RTLDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);

            // Act
            db.TVShows.Add(show1);
            db.TVShows.Add(show2);
            db.TVShows.Add(show3);
            db.SaveChanges();

            // Assert
            var actors = db.Actors.ToArray();
            actors.Should().HaveCount(5);
            actors.FirstOrDefault(s => s.Id == 2).ShowCasts.Should().HaveCount(2);
        }

    }
}
