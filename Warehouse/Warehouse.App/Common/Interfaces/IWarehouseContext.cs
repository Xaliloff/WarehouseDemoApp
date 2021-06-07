using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.App.Common.Interfaces
{
    public interface IWarehouseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WarehouseTransaction> WarehouseTransactions { get; set; }
        public DbSet<IssueTransaction> IssueTransactions { get; set; }
        public DbSet<ReceiptTransaction> ReceiptTransactions { get; set; }
        public DbSet<GoodsIssueNote> GoodsIssueNotes { get; set; }
        public DbSet<GoodsIssueNoteItem> GoodsIssueNoteItems { get; set; }
        public DbSet<GoodsReceiptNote> GoodsReceiptNotes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<GoodsReceiptNoteItem> GoodsReceiptNoteItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        public DatabaseFacade Database { get; }
    }
}