using System;
using System.Collections.Generic;
using Warehouse.Domain.Common;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class GoodsIssueNote : AuditableEntity
    {
        public GoodsIssueNote()
        {
            Id = Guid.NewGuid().ToString();
            GinItems = new List<GoodsIssueNoteItem>();
        }
        public string Id { get; set; }
        public string SignedBy { get; set; }
        public DocumentState DocumentState { get; set; }
        public List<GoodsIssueNoteItem> GinItems { get; set; }
        public string Comment { get; set; }
    }
}