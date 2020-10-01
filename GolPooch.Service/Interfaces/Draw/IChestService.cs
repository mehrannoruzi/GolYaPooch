using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.Service.Interfaces
{
    public interface IChestService
    {
        /// <summary>
        /// Returns Available chest
        /// this method must be cached
        /// </summary>
        /// <returns>list of available chest</returns>
        IResponse<List<Chest>> GetAllAvailable();
        IResponse<int> MyChanceAsync(int userId, int ChestId);
        Task<IResponse<string>> SpendChanceAsync(int userId, int purchaseId, int ChestId, byte chanceCount);
    }
}