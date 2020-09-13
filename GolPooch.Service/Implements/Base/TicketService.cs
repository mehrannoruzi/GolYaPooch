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

        public IResponse<PagingListDetails<Ticket>> Get(int userId, PagingParameter pagingParameter)
        {
            var response = new Response<PagingListDetails<Ticket>>();
            try
            {
                var tickets = _appUow.TicketRepo.GetPaging(
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

        public async Task<IResponse<Ticket>> Get(int ticketId)
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

        public async Task<IResponse<bool>> Read(int ticketId)
        {
            var response = new Response<bool>();
            try
            {
                var ticket = await _appUow.TicketRepo.FirstOrDefaultAsync(
                    new QueryFilter<Ticket>
                    {
                        AsNoTracking = false,
                        Conditions = x => x.TicketId == ticketId
                    });
                if (ticket == null) return new Response<bool> { Message = ServiceMessage.InvalidTicketId };


                ticket.IsRead = true;
                _appUow.TicketRepo.Update(ticket);
                var saveResult = await _appUow.ElkSaveChangesAsync();

                response.IsSuccessful = saveResult.IsSuccessful;
                response.Result = saveResult.IsSuccessful;
                response.Message = saveResult.Message;
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
