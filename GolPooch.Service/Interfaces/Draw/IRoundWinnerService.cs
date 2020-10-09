using Elk.Core;
using GolPooch.Domain.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IRoundWinnerService
    {
        Task<IResponse<IEnumerable<WinnerDto>>> GetLastWinnerAsync(PagingParameter pagingParameter);
        Task<IResponse<IEnumerable<WinnerDto>>> GetMustWinnerAsync(PagingParameter pagingParameter);
    }
}