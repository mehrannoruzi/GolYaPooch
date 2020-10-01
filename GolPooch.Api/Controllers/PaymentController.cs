using GolPooch.Api.Models;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Elk.Core;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private IPaymentService _paymentService { get; set; }

        public PaymentController(IPaymentService paymentService)
            => _paymentService = paymentService;


        [HttpGet]
        public IActionResult AllGateway()
            => Ok(_paymentService.GetAllGateway());

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user, [FromBody] PaymentTransaction paymentTransaction)
        {
            paymentTransaction.UserId = user.UserId;
            var createPaymentResult = await _paymentService.CreateAsync(paymentTransaction);

            if (createPaymentResult.IsSuccessful)
                return Redirect(createPaymentResult.Result);
            else
                return Json(new Response<string> { Message = createPaymentResult.Message });
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