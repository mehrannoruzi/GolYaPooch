using Elk.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class ChangeLogController : Controller
    {
        private IChangeLogService _changeLogService { get; set; }

        public ChangeLogController(IChangeLogService changeLogService)
            => _changeLogService = changeLogService;


        [HttpGet]
        public async Task<JsonResult> GetAsync(string version)
            => Json(await _changeLogService.GetAsync(version));

        [HttpGet]
        public async Task<JsonResult> TopAsync([FromQuery] PagingParameter pagingParameter)
            => Json(await _changeLogService.GetTopChangeAsync(pagingParameter));
    }
}