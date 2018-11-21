using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTLTest.Services;

namespace RTLTestWebApi.Controllers
{
    [Route("shows")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IDataService _dataService;

        public MainController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET /shows/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _dataService.GetShow(id));
        }

        // GET /shows/list?page=1&pageSize=20
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 20)
        {
            return Ok(await _dataService.GetShowList(page, pageSize));
        }

    }
}
