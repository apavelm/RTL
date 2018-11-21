using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using RTLTest.Services.AutoMapper;
using RTLTestTask.ApiModels;

namespace RTLTest.Services
{
    public class ScrapperService: IScrapperService
    {
        private readonly ITVMazeClient _mazeClient;
        private readonly ModelMapper _modelMapper;

        public ScrapperService(ITVMazeClient mazeClient, ModelMapper modelMapper)
        {
            _mazeClient = mazeClient;
            _modelMapper = modelMapper;
        }

        public async Task<TVShowResponse> ScrapTheShow(int id, CancellationToken cancellationToken)
        {
            var show = await _mazeClient.GetTheShowWithActors(id, cancellationToken);

            return _modelMapper.GetShowResponse(show);
        }

        public async Task ScrapPage(int page, Action<TVShowResponse> actionSaveShow, CancellationToken cancellationToken)
        {
            try
            {
                var showList = await _mazeClient.GetAllShows(page, cancellationToken);
                foreach (var show in showList)
                {
                    await Task.Delay(600, cancellationToken);

                    var cast = await _mazeClient.GetShowCasts(show.Id, cancellationToken);

                    actionSaveShow(_modelMapper.GetShowResponse(show, cast));
                }
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    // end of the job
                }
            }
        }
    }
}
