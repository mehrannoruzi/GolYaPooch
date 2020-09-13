using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface ITicketService
    {
        /// <summary>
        /// Add ticket for user
        /// </summary>
        /// <param name="model">model that is filled by user in clientside</param>
        /// <param name="userId">id of current user</param>
        /// <returns>return ticket primary key</returns>
        Task<IResponse<int>> AddAsync(int userId, Ticket model);

        /// <summary>
        /// Get Top 10 tickets of user
        /// </summary>
        /// <param name="userId">userid that is filled from jwt</param>
        /// <returns>lsit of tickets model</returns>
        IResponse<PagingListDetails<Ticket>> Get(int userId, PagingParameter pagingParameter);

        /// <summary>
        /// Get ticket with userid and ticket parameter
        /// </summary>
        /// <param name="userid">userid that is filled from jwt</param>
        /// <param name="ticketId">ticket primary key</param>
        /// <returns>list of ticket model</returns>
        Task<IResponse<Ticket>> Get(int ticketId);

        /// <summary>
        /// When user tapped in a ticket, this method called to set isread field with true status.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        Task<IResponse<bool>> Read(int ticketId);
    }
}