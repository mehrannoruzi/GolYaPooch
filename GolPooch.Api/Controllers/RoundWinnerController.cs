using Elk.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    public class RoundWinnerController : Controller
    {
        private IRoundWinnerService _roundWinnerService { get; set; }

        public RoundWinnerController(IRoundWinnerService roundWinnerService)
            => _roundWinnerService = roundWinnerService;


        [HttpGet]
        public async Task<JsonResult> LastWinnersAsync([FromQuery] PagingParameter pagingParameter)
            => Json(await _roundWinnerService.GetLastWinnerAsync(pagingParameter));

        [HttpGet]
        public async Task<JsonResult> MustWinnersAsync([FromQuery] PagingParameter pagingParameter)
            => Json(await _roundWinnerService.GetMustWinnerAsync(pagingParameter));

    }
}