using System;
using Warehouse.Domain.Common;

namespace Warehouse.Domain.Entities
{
    abstract public class WarehouseTransaction : AuditableEntity
    {
        public string Id { get; private set; }
        public string ProductId { get; private set; }
        public Product Product { get; private set; }
        public int BalanceChange { get; private set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; private set; }

        public WarehouseTransaction(string productId)
        {
            Id = Guid.NewGuid().ToString();
            TransactionDate = DateTime.Now;
            ProductId = productId;
        }

        public void SetBalanceChange(int count)
        {
            if (count <= 0) throw new ArgumentException();

            if (this is IssueTransaction)
            {
                BalanceChange = -count;
                return;
            }
            else if (this is ReceiptTransaction)
            {
                BalanceChange = count;
                return;
            }

            throw new InvalidOperationException();
        }
    }
}