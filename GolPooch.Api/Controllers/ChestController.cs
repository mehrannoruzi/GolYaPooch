using GolPooch.Api.Models;
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
        {
            _chestService = chestService;
        }


        [HttpGet]
        public IActionResult All()
            => Ok(_chestService.GetAllAvailable());

        [HttpGet]
        public IActionResult MyChanceAsync(User user, [FromBody] MyChanceModel myChanceModel)
            => Ok(_chestService.MyChanceAsync(user.UserId, myChanceModel.ChestId));

        [HttpPost]
        public async Task<JsonResult> SpendChanceAsync(User user, [FromBody] SpendChanseModel spendChanse)
            => Json(await _chestService.SpendChanceAsync(user.UserId, spendChanse.PurchaseId, spendChanse.ChestId, spendChanse.ChanceCount));


    }
}