using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class PurchaseController : Controller
    {
        private IPurchaseService _purchaseService { get; set; }

        public PurchaseController(IPurchaseService purchaseService)
            => _purchaseService = purchaseService;


        [HttpGet]
        public async Task<JsonResult> ActiveAsync(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _purchaseService.GetActivePurchasesAsync(user.UserId, pagingParameter));

        [HttpGet]
        public async Task<JsonResult> AllAsync(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _purchaseService.GetAllPurchasesAsync(user.UserId, pagingParameter));

        [HttpPost]
        public async Task<ActionResult> PaybackAsync()
        {
            await _purchaseService.ProccessPaybackblePurchaseAsync();
            return Ok();
        }

    }
}