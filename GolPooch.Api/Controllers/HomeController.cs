using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GolPooch.Api.Controllers
{
    [AuthFilter, Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() => Ok("Welcome to GolYaPooch Api ...");
    }
}