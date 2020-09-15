using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private IProductService _productService { get; set; }

        public ProductController(IProductService productService)
            => _productService = productService;


        [HttpGet]
        public IActionResult All()
            => Ok(_productService.GetAllAvailable());
    }
}