using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GolPooch.Service.Interfaces;

namespace GolPooch.Api.Controllers
{
    [AuthorizeFilter, Route("[controller]/[action]")]
    public class TicketController : Controller
    {
        private ITicketService _ticketService { get; set; }

        public TicketController(ITicketService ticketService)
            => _ticketService = ticketService;

        [HttpPost]
        public async Task<JsonResult> AddAsync(User user, [FromBody] Ticket ticket)
            => Json(await _ticketService.AddAsync(user.UserId, ticket));

        [HttpGet]
        public async Task<JsonResult> GetAsync(int ticketId)
            => Json(await _ticketService.GetAsync(ticketId));

        [HttpPost]
        public async Task<JsonResult> ReadAsync(User user, int ticketId)
            => Json(await _ticketService.ReadAsync(user.UserId, ticketId));

        [HttpGet]
        public async Task<JsonResult> UnReadCountAsync(User user)
            => Json(await _ticketService.UnReadCountAsync(user.UserId));

        [HttpGet]
        public async Task<JsonResult> TopAsync(User user, [FromQuery] PagingParameter pagingParameter)
            => Json(await _ticketService.GetTopTicketAsync(user.UserId, pagingParameter));

    }
}