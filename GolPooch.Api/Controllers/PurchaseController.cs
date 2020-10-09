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
        public async Task<JsonResult> Top(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _purchaseService.GetTopPurchases(user.UserId, pagingParameter));

        [HttpGet]
        public async Task<JsonResult> All(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _purchaseService.GetAllPurchases(user.UserId, pagingParameter));


    }
}