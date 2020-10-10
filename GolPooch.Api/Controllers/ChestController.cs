using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class ChestController : Controller
    {
        private IChestService _chestService { get; set; }

        public ChestController(IChestService chestService)
            => _chestService = chestService;


        [HttpGet]
        public async Task<IActionResult> AllAsync()
            => Ok(await _chestService.GetAllAvailableAsync());

        [HttpGet]
        public async Task<IActionResult> MyChanceCountAsync(User user, int chestId)
            => Ok(await _chestService.MyChanceCountAsync(user.UserId, chestId));

        [HttpPost]
        public async Task<JsonResult> SpendChanceAsync(User user, [FromBody] SpendChanseModel spendChanse)
            => Json(await _chestService.SpendChanceAsync(user.UserId, spendChanse.PurchaseId, spendChanse.ChestId, spendChanse.ChanceCount));

    }
}