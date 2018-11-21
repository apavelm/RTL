using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using RTLTestTask.ApiModels;

namespace RTLTest.Services
{
    public interface ITVMazeClient
    {
        [Get("/shows/{id}")]
        Task<TVShow> GetTheShow(int id, CancellationToken cancellationToken);

        [Get("/shows/{id}/cast")]
        Task<List<Cast>> GetShowCasts(int id, CancellationToken cancellationToken);

        [Get("/shows/{id}?embed=cast")]
        Task<TVShowWithEmbed<CastEmbedList>> GetTheShowWithActors(int id, CancellationToken cancellationToken);

        [Get("/shows")]
        Task<List<TVShow>> GetAllShows(int page, CancellationToken cancellationToken);
    }
}
