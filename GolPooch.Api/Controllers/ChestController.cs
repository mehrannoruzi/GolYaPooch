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

        /// <summary>
        /// Get All Availible Chest
        /// </summary>
        /// <returns>List of availible chest</returns>
        [HttpGet]
        public IActionResult All()
            => Ok(_chestService.GetAllAvailable());
    }
}