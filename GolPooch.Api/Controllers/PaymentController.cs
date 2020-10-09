using Elk.Core;
using GolPooch.Api.Models;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private IPaymentService _paymentService { get; set; }

        public PaymentController(IPaymentService paymentService)
            => _paymentService = paymentService;


        [HttpGet]
        public async Task<IActionResult> AllGateway()
            => Ok(await _paymentService.GetAllGateway());

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user, [FromBody] PaymentTransaction paymentTransaction)
        {
            paymentTransaction.UserId = user.UserId;
            var createPaymentResult = await _paymentService.CreateAsync(paymentTransaction);

            //if (createPaymentResult.IsSuccessful)
            //    return Json(createPaymentResult.Result);
            //else
            //    return Json(new Response<string> { Message = createPaymentResult.Message });

            var verifyPaymentResult = await _paymentService.VerifyAsync(paymentTransaction.PaymentTransactionId, "Ok", "123456789");
            return Json(verifyPaymentResult);
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ZarinPalVerifyAsync([FromQuery] PaymentVerifyModel paymentVerifyModel)
        {
            var verifyPaymentResult = await _paymentService.VerifyAsync(paymentVerifyModel.PaymentTransactionId, paymentVerifyModel.Status, paymentVerifyModel.Authority);

            if (verifyPaymentResult.IsSuccessful)
                return Redirect(verifyPaymentResult.Result);
            else
                return Json(new Response<string> { Message = verifyPaymentResult.Message });
        }
    }
}