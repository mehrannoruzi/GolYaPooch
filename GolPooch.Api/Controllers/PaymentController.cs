using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

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
    }
}