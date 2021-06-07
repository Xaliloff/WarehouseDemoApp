using Warehouse.Domain.Common;

namespace Warehouse.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public string Id { get; set; }
    }
}