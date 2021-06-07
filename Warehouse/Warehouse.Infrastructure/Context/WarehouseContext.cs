using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.App.Services;
using Warehouse.Domain.Common;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.Context
{
    public class WarehouseContext : DbContext, IWarehouseContext
    {
        private readonly ICurrentUserService _currentUserService;

        public WarehouseContext(DbContextOptions options,
            ICurrentUserService currentUserService)
                : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WarehouseTransaction> WarehouseTransactions { get; set; }
        public DbSet<IssueTransaction> IssueTransactions { get; set; }
        public DbSet<ReceiptTransaction> ReceiptTransactions { get; set; }
        public DbSet<GoodsIssueNote> GoodsIssueNotes { get; set; }
        public DbSet<GoodsIssueNoteItem> GoodsIssueNoteItems { get; set; }
        public DbSet<GoodsReceiptNote> GoodsReceiptNotes { get; set; }
        public DbSet<GoodsReceiptNoteItem> GoodsReceiptNoteItems { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var userId = _currentUserService.UserId;
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = $"({userId})";
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = $"({userId})";
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}