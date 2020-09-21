﻿using Elk.Core;
using Elk.AspNetCore;
using GolPooch.Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using GolPooch.Service.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
        public async Task<JsonResult> GetCodeAsync([FromBody] VerifyCodeModel model)
            => Json(await _authenticateService.GetCodeAsync(model.MobileNumber));

        [HttpPost]
        public async Task<JsonResult> VerifyCodeAsync([FromServices] IWebHostEnvironment env, [FromBody] VerifyCodeModel model)
        {
            var response = new Response<JwtToken>();
            if (env.IsDevelopment())
            {
                var userClaims = new List<Claim> {
                     new Claim("UserId", "4"),
                     new Claim("MobileNumber", "9334188184"),
                };
                response.IsSuccessful = true;
                response.Result = _jwtService.CreateToken(userClaims, _jwtSettings);
                return Json(response);
            }
            var verifyResult = await _authenticateService.VerifyCodeAsync(model.TransactionId, model.PinCode, HttpContext);
            if (verifyResult.IsSuccessful)
            {
                var userClaims = new List<Claim> {
                     new Claim("UserId", verifyResult.Result.UserId.ToString()),
                     new Claim("MobileNumber", verifyResult.Result.MobileNumber.ToString()),
                };

                response.IsSuccessful = true;
                response.Message = verifyResult.Message;
                response.Result = _jwtService.CreateToken(userClaims, _jwtSettings);
            }
            else
            {
                response.Message = verifyResult.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> ResendCodeAsync([FromBody] VerifyCodeModel model)
            => Json(await _authenticateService.GetCodeAsync(model.MobileNumber));
    }
}