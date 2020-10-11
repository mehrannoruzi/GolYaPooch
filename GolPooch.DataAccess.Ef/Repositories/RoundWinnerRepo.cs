using Elk.Core;
using GolPooch.Domain;
using GolPooch.Domain.Dto;
using System.Threading.Tasks;
using GolPooch.Domain.Entity;
using Elk.EntityFrameworkCore;
using System.Collections.Generic;

namespace GolPooch.DataAccess.Ef
{
    public class RoundWinnerRepo : EfGenericRepo<RoundWinner>, IRoundWinnerRepo
    {
        private readonly AppDbContext _appDbContext;

        public RoundWinnerRepo(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<WinnerDto>> GetLastWinnersAsync(PagingParameter pagingParameter)
            => await _appDbContext.ExecuteSpListAsync<WinnerDto>("EXEC [Draw].[GetLastWinner]", pagingParameter.PageNumber, pagingParameter.PageSize);

        public async Task<IEnumerable<WinnerDto>> GetMustWinnersAsync(PagingParameter pagingParameter)
            => await _appDbContext.ExecuteSpListAsync<WinnerDto > ("EXEC [Draw].[GetMustWinner]", pagingParameter.PageNumber, pagingParameter.PageSize);

    }
}