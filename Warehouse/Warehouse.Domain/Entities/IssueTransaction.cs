namespace Warehouse.Domain.Entities
{
    public class IssueTransaction : WarehouseTransaction
    {
        public IssueTransaction(string productId) : base(productId)
        {
        }
    }
}