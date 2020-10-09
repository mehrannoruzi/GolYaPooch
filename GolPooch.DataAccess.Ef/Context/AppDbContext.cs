using Elk.Core;
using GolPooch.Domain.Entity;
using Elk.EntityFrameworkCore;
using Elk.EntityFrameworkCore.Tools;
using Microsoft.EntityFrameworkCore;
using GolPooch.Domain.Dto;

namespace GolPooch.DataAccess.Ef
{
    public partial class AppDbContext : ElkDbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(x => x.MobileNumber).IsUnique();
            builder.Entity<Page>().HasIndex(x => x.Address).IsUnique();
            builder.Entity<PushEndpoint>().HasIndex(x => x.PushKey).IsUnique();

            builder.Entity<WinnerDto>().HasNoKey().ToTable(null);

            builder.OverrideDeleteBehavior();
            builder.RegisterAllEntities<IEntity>(typeof(User).Assembly);
        }

        public DbSet<WinnerDto> WinnerDtos { get; set; }
    }
}