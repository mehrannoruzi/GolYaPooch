using Elk.EntityFrameworkCore;

namespace GolPooch.DataAccess.Ef
{
    public class GenericRepo<T> : EfGenericRepo<T> where T : class
    {
        public GenericRepo(AppDbContext appDbContext) : base(appDbContext) { }
    }
}