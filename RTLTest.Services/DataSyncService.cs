using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RTLTestTask.ApiModels;
using RTLTestTask.Db;
using RTLTestTask.Models;
using Cast = RTLTestTask.Models.Cast;
using TVShow = RTLTestTask.Models.TVShow;

namespace RTLTest.Services
{
    public class DataSyncService: IHostedService
    {
        private readonly IServiceScopeFactory _scopedFactory;
        private readonly IScrapperService _scrapperService;

        public DataSyncService(IServiceScopeFactory scopedFactory, IScrapperService scrapperService)
        {
            _scopedFactory = scopedFactory;
            _scrapperService = scrapperService;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            using (var scope = _scopedFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<RTLDbContext>();

                while (true)
                {
                    var lastId = 0;
                    if (await db.TVShows.AnyAsync(cancellationToken))
                    {
                        lastId = await db.TVShows.AsNoTracking().MaxAsync(s => s.Id, cancellationToken);
                    }

                    var page = 1 + (int) (lastId / 250);

                    await _scrapperService.ScrapPage(page, async response => { await SaveShowData(db, response, cancellationToken); } , cancellationToken);
                }
            }
        }

        private async Task SaveShowData(RTLDbContext db, TVShowResponse showItem, CancellationToken cancellationToken)
        {
            var showCast = new List<ShowCast>();
            var duplicateSet = new HashSet<int>();
            foreach (var cast in showItem.Casts)
            {
                if (duplicateSet.Contains(cast.Id)) continue;
                if (await db.Actors.AnyAsync(s => s.Id == cast.Id, cancellationToken))
                {
                    if (!await db.TVShowCast
                        .Where(s => s.CastId == cast.Id && s.ShowId == showItem.Id)
                        .AnyAsync(cancellationToken))
                    {
                        db.TVShowCast.Add(new ShowCast() { CastId = cast.Id, ShowId = showItem.Id });
                    }
                }
                else
                {
                    showCast.Add(new ShowCast()
                    {
                        Cast = new Cast()
                        {
                            Name = cast.Name,
                            Id = cast.Id,
                            DoB = cast.DoB
                        }
                    });
                }

                duplicateSet.Add(cast.Id);
            }

            var show = new TVShow()
            {
                Id = showItem.Id,
                Name = showItem.Name,
                ShowCasts = showCast
            };

            db.TVShows.Add(show);
            await db.SaveChangesAsync(cancellationToken);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
