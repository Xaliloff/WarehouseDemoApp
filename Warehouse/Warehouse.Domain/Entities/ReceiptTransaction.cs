namespace Warehouse.Domain.Entities
{
    public class ReceiptTransaction : WarehouseTransaction
    {
        public ReceiptTransaction(string productId) : base(productId)
        {
        }
    }
}