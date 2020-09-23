using GolPooch.Domain.Dto;
using GolPooch.Api.Models;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
            => _userService = userService;


        [HttpPost]
        public async Task<JsonResult> UpdateProfileAsync(User user, [FromBody] UserDto userDto)
            => Json(await _userService.UpdateProfileAsync(user.UserId, userDto));

        [HttpPost]
        public async Task<JsonResult> UploadAwatarAsync(User user, [FromBody] FileModel fileModel)
            => Json(await _userService.UploadAwatarAsync(user.UserId, fileModel.FileName, fileModel.FileBytes));

    }
}