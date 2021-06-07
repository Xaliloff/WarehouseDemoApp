using System;
using System.Collections.Generic;
using Warehouse.Domain.Common;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class GoodsReceiptNote : AuditableEntity
    {
        public GoodsReceiptNote()
        {
            Id = Guid.NewGuid().ToString();
            GrnItems = new();
        }
        public string Id { get; set; }
        public string AcceptedBy { get; set; }
        public string VendorName { get; set; }
        public DocumentState DocumentState { get; set; }
        public List<GoodsReceiptNoteItem> GrnItems { get; set; }
    }
}