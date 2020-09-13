using System;
using System.Threading;
using System.Threading.Tasks;
using Elk.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GolPooch.DataAccess.Ef
{
    public partial class AppUnitOfWork
    {
        private readonly AppDbContext _appDbContext; private readonly IServiceProvider _serviceProvider;

        public AppUnitOfWork(AppDbContext golPoochDbContext, 
            IServiceProvider serviceProvider)
        {
            _appDbContext = golPoochDbContext;
            _serviceProvider = serviceProvider;
        }

        public ChangeTracker ChangeTracker { get => _appDbContext.ChangeTracker; }
        public DatabaseFacade Database { get => _appDbContext.Database; }

        public SaveChangeResult ElkSaveChanges() => _appDbContext.ElkSaveChanges();

        public Task<SaveChangeResult> ElkSaveChangesAsync(CancellationToken cancellationToken = default)
            => _appDbContext.ElkSaveChangesAsync(cancellationToken);

        public SaveChangeResult ElkSaveChangesWithValidation()
            => _appDbContext.ElkSaveChangesWithValidation();

        public Task<SaveChangeResult> ElkSaveChangesWithValidationAsync(CancellationToken cancellationToken = default)
            => _appDbContext.ElkSaveChangesWithValidationAsync(cancellationToken);

        public int SaveChanges()
            => _appDbContext.SaveChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _appDbContext.SaveChangesAsync(cancellationToken);

        public Task<int> SaveChangesAsync(bool AcceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
            => _appDbContext.SaveChangesAsync(AcceptAllChangesOnSuccess, cancellationToken);

        public void Dispose() => _appDbContext.Dispose();
    }
}