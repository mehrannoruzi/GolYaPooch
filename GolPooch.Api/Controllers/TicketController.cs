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
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<JsonResult> AddAsync(User user, [FromBody] Ticket ticket)
            => Json(await _ticketService.AddAsync(user.UserId, ticket));

        [HttpGet]
        public JsonResult Top(User user, [FromBody] PagingParameter pagingParameter)
            => Json(_ticketService.Get(user.UserId, pagingParameter));

        [HttpGet]
        public async Task<JsonResult> Get(int ticketId)
            => Json(await _ticketService.Get(ticketId));

        [HttpPost]
        public async Task<JsonResult> Read(int ticketId)
            => Json(await _ticketService.Read(ticketId));
    }
}