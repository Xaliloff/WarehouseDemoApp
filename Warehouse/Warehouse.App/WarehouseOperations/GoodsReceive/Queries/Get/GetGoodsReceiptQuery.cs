using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseOperations.GoodsReceive.Queries.Get
{
    public class GetGoodsReceiptQuery : IRequest<CommandResponse<List<GoodsReceiptNote>>>
    {
    }

    public class GetGoodsReceiptQueryHandler : IRequestHandler<GetGoodsReceiptQuery, CommandResponse<List<GoodsReceiptNote>>>
    {
        private readonly IWarehouseContext _context;

        public GetGoodsReceiptQueryHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<List<GoodsReceiptNote>>> Handle(GetGoodsReceiptQuery request, CancellationToken cancellationToken)
        {
            var grns = await _context.GoodsReceiptNotes.ToListAsync();
            return new(grns);
        }
    }
}