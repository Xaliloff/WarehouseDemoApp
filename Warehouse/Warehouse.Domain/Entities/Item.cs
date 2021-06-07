using System;
using Warehouse.Domain.Common;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class Item : AuditableEntity
    {
        public Item()
        {
            Id = Guid.NewGuid().ToString();
            Condition = Condition.Normal;
        }
        public string Id { get; set; }
        public Condition Condition { get; set; }
        public string Comment { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}