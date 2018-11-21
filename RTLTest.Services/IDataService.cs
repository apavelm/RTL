using System.Collections.Generic;
using System.Threading.Tasks;
using RTLTestTask.ApiModels;

namespace RTLTest.Services
{
    public interface IDataService
    {
        Task<TVShowResponse> GetShow(int id);

        Task<List<TVShowResponse>> GetShowList(int page, int pageSize);
    }
}