using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Approve
{
    public record ApproveGoodsReceiptCommand(string id) : IRequest<CommandResponse>
    {
    }

    public class ApproveGoodsReceiptHandler : IRequestHandler<ApproveGoodsReceiptCommand, CommandResponse>
    {
        private readonly IWarehouseContext _context;
        private readonly IPublishEndpoint _bus;

        public ApproveGoodsReceiptHandler(IWarehouseContext context,
            IPublishEndpoint bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<CommandResponse> Handle(ApproveGoodsReceiptCommand request, CancellationToken cancellationToken)
        {
            var grn = await _context.GoodsReceiptNotes
                .Include(x => x.GrnItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == request.id);

            grn.DocumentState = DocumentState.Approved;

            var items = new List<Item>();
            var receiptTransactions = new List<ReceiptTransaction>();
            grn.GrnItems.ForEach(x =>
            {
                AddItemsGRNItem(items, x);
                AddWarehouseTransaction(receiptTransactions, x);
            });

            _context.GoodsReceiptNotes.Update(grn);
            await _context.Items.AddRangeAsync(items, cancellationToken);
            await _context.ReceiptTransactions.AddRangeAsync(receiptTransactions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new();
        }

        private void AddWarehouseTransaction(List<ReceiptTransaction> receiptTransactions, GoodsReceiptNoteItem item)
        {
            var receiptTransaction = new ReceiptTransaction(item.ProductId);
            receiptTransaction.SetBalanceChange(item.Quantity);
            receiptTransactions.Add(receiptTransaction);
        }

        public void AddItemsGRNItem(List<Item> items, GoodsReceiptNoteItem item)
        {
            for (int i = 0; i < item.Quantity; i++)
            {
                items.Add(new() { ProductId = item.ProductId });
            }
        }
    }
}