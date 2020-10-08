using Elk.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IRoundService
    {
        Task<IResponse<PagingListDetails<object>>> GetTopRoundsAsync(int userId, PagingParameter pagingParameter);
        Task<IResponse<List<object>>> GetChanceInRoundAsync(int roundId);
    }
}