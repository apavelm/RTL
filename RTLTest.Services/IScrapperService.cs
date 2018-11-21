using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RTLTestTask.ApiModels;

namespace RTLTest.Services
{
    public interface IScrapperService
    {
        Task<TVShowResponse> ScrapTheShow(int id, CancellationToken cancellationToken);

        Task ScrapPage(int page, Action<TVShowResponse> actionSaveShow, CancellationToken cancellationToken);
    }
}