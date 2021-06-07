using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Create
{
    public class CreateGoodsReceiptCommand : IRequest<CommandResponse>
    {
        public string VendorName { get; set; }
        public List<Good> Goods { get; set; }
        public string AcceptedBy { get; set; }

        public class Good
        {
            public string ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class CreateGoodsReceiptHandler : IRequestHandler<CreateGoodsReceiptCommand, CommandResponse>
    {
        private readonly IWarehouseContext _context;

        public CreateGoodsReceiptHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse> Handle(CreateGoodsReceiptCommand request, CancellationToken cancellationToken)
        {
            var grn = new GoodsReceiptNote()
            {
                DocumentState = DocumentState.Draft,
                VendorName = request.VendorName,
                AcceptedBy = request.AcceptedBy,
                GrnItems = request.Goods
                        .Select(x => new GoodsReceiptNoteItem() { ProductId = x.ProductId, Quantity = x.Quantity })
                        .ToList()
            };
            await _context.GoodsReceiptNotes.AddAsync(grn, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new();
        }
    }
}