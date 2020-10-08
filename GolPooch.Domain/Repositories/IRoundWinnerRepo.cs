using Elk.Core;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Domain
{
    public interface IRoundWinnerRepo : IGenericRepo<RoundWinner>
    {
        Task<IEnumerable<WinnerDto>> GetLastWinnersAsync(PagingParameter pagingParameter);
        Task<IEnumerable<WinnerDto>> GetMustWinnersAsync(PagingParameter pagingParameter);
    }
}