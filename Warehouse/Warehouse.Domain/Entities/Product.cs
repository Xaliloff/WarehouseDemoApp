using System.Collections.Generic;
using Warehouse.Domain.Common;
using Warehouse.Domain.Entities;

namespace Warehouse.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int CurrentStockQuantity { get; set; }
        public List<Item> Items { get; set; }
    }
}