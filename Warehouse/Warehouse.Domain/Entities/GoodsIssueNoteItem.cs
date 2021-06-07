using System;
using Warehouse.Domain.Common;

namespace Warehouse.Domain.Entities
{
    public class GoodsIssueNoteItem : AuditableEntity
    {
        public GoodsIssueNoteItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string GoodIssueNoteId { get; set; }
        public GoodsIssueNote GoodIssueNote { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
}