using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class RoundController : Controller
    {
        private IRoundService _roundService { get; set; }

        public RoundController(IRoundService roundService)
            => _roundService = roundService;


        [HttpGet]
        public async Task<JsonResult> TopAsync(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _roundService.GetTopRoundsAsync(user.UserId, pagingParameter));

        [HttpGet]
        public async Task<JsonResult> MyChanceAsync([FromQuery] int roundId)
            => Json(await _roundService.GetChanceInRoundAsync(roundId));

    }
}