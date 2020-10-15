using Elk.Core;
using Elk.AspNetCore;
using GolPooch.Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace GolPooch.Api.Controllers
{
    [AuthFilter, Route("[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private IAuthenticateService _authenticateService { get; }

        public AuthenticationController(IAuthenticateService authenticateService,
            IJwtService jwtService, IOptions<JwtSettings> jwtSettings)
        {
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public async Task<JsonResult> GetCodeAsync([FromBody] AuthenticationModel model)
            => Json(await _authenticateService.GetCodeAsync(model.MobileNumber));

        [HttpPost]
        public async Task<JsonResult> VerifyCodeAsync([FromBody] AuthenticationModel model)
        6{
            var response = new Response<JwtToken>();
            var verifyResult = await _authenticateService.VerifyCodeAsync(model.TransactionId, model.PinCode, HttpContext);
            if (verifyResult.IsSuccessful)
            {
                var userClaims = new List<Claim> {
                     new Claim("UserId", verifyResult.Result.UserId.ToString()),
                     new Claim("MobileNumber", verifyResult.Result.MobileNumber.ToString()),
                };

                var createTokenResult = _jwtService.CreateToken(userClaims, _jwtSettings);
                var updateTokenResult = await _authenticateService.UpdateRefreshToken(verifyResult.Result.UserId, createTokenResult.RefreshToken);

                response.Result = createTokenResult;
                response.Message = updateTokenResult.Message;
                response.IsSuccessful = updateTokenResult.IsSuccessful;
            }
            else
            {
                response.Message = verifyResult.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ResendCodeAsync([FromBody] AuthenticationModel model)
            => Json(await _authenticateService.GetCodeAsync(model.MobileNumber));

        [HttpPost]
        public async Task<JsonResult> RefreshTokenAsync([FromBody] AuthenticationModel model)
        {
            var response = new Response<JwtToken>();
            var authenticateResult = await _authenticateService.Authenticate(model.RefreshToken);
            if (authenticateResult.IsSuccessful)
            {
                var userClaims = new List<Claim> {
                     new Claim("UserId", authenticateResult.Result.UserId.ToString()),
                     new Claim("MobileNumber", authenticateResult.Result.MobileNumber.ToString()),
                };

                var createTokenResult = _jwtService.CreateToken(userClaims, _jwtSettings);
                var updateTokenResult = await _authenticateService.UpdateRefreshToken(authenticateResult.Result.UserId, createTokenResult.RefreshToken);

                response.Result = createTokenResult;
                response.Message = updateTokenResult.Message;
                response.IsSuccessful = updateTokenResult.IsSuccessful;
            }
            else
            {
                response.Message = authenticateResult.Message;
            }

            return Json(response);
        }
    }
}