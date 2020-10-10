using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class BannerController : Controller
    {
        private IBannerService _bannerService { get; set; }

        public BannerController(IBannerService bannerService)
            => _bannerService = bannerService;

        [HttpGet]
        public async Task<JsonResult> All()
            => Json(await _bannerService.GetAllAvailable());
    }
}