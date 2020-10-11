using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface IChangeLogService : IScopedInjection
    {
        Task<IResponse<ChangeLog>> GetAsync(string version);
        Task<IResponse<PagingListDetails<ChangeLog>>> GetTopChangeAsync(PagingParameter pagingParameter);
    }
}