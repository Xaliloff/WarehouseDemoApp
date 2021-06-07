using System;
using Warehouse.Domain.Common;

namespace Warehouse.Domain.Entities
{
    public class GoodsReceiptNoteItem : AuditableEntity
    {
        public GoodsReceiptNoteItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string GoodsReceiptNoteId { get; set; }
        public GoodsReceiptNote GoodsReceiptNote { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}