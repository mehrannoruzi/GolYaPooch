using System;
using Elk.Core;
using System.Linq;
using System.Threading.Tasks;
using GolPooch.DataAccess.Ef;
using GolPooch.Domain.Entity;
using GolPooch.Service.Resourses;
using GolPooch.Service.Interfaces;

namespace GolPooch.Service.Implements
{
    public class TicketService : ITicketService
    {
        private AppUnitOfWork _appUow { get; set; }

        public TicketService(AppUnitOfWork appUnitOfWork)
        {
            _appUow = appUnitOfWork;
        }

        public async Task<IResponse<int>> AddAsync(int userId, Ticket model)
        {
            var response = new Response<int>();
            try
            {
                model.UserId = userId;
                model.IsRead = true;
                await _appUow.TicketRepo.AddAsync(model);
                var saveResult = await _appUow.ElkSaveChangesAsync();
                response.Message = saveResult.Message;
                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful ? model.TicketId : 0;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<Ticket>> GetAsync(int ticketId)
        {
            var response = new Response<Ticket>();
            try
            {
                var tickets = await _appUow.TicketRepo.FirstOrDefaultAsync(
                    new QueryFilter<Ticket>
                    {
                        Conditions = x => x.TicketId == ticketId
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = tickets;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<int>> ReadAsync(int userId, int ticketId)
        {
            var response = new Response<int>();
            try
            {
                var ticket = await _appUow.TicketRepo.FirstOrDefaultAsync(
                    new QueryFilter<Ticket>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.TicketId == ticketId
                    });
                if (ticket == null) return new Response<int> { Message = ServiceMessage.InvalidTicketId };
                if (ticket.UserId != userId) return new Response<int> { Message = ServiceMessage.InvalidParameter };

                ticket.IsRead = true;
                _appUow.TicketRepo.Update(ticket);
                var saveResult = await _appUow.ElkSaveChangesAsync();
                if(!saveResult.IsSuccessful) return new Response<int> { Message = saveResult.Message };
                //response.IsSuccessful = saveResult.IsSuccessful;
                //response.Result = saveResult.IsSuccessful;
                //response.Message = saveResult.Message;
                return await UnReadCountAsync(userId);
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<int>> UnReadCountAsync(int userId)
        {
            var response = new Response<int>();
            try
            {
                response.Result = await _appUow.TicketRepo.CountAsync(
                    new QueryFilter<Ticket>
                    {
                        Conditions = x => x.UserId == userId && !x.IsRead
                    });

                response.IsSuccessful = true;
                response.Message = ServiceMessage.Success;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<PagingListDetails<Ticket>>> GetTopTicketAsync(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<Ticket>>();
            try
            {
                var tickets = await _appUow.TicketRepo.GetPagingAsync(
                    new PagingQueryFilter<Ticket>
                    {
                        Conditions = x => x.UserId == userId,
                        PagingParameter = pagingParameter,
                        OrderBy = x => x.OrderByDescending(x => x.TicketId)
                    });

                response.Message = ServiceMessage.Success;
                response.IsSuccessful = true;
                response.Result = tickets;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = ServiceMessage.Exception;
                return response;
            }
        }
    }
}
