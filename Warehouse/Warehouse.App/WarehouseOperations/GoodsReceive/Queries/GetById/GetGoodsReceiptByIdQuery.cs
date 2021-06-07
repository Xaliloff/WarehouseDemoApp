using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseOperations.GoodsReceive.Queries.GetById
{
    public record GetGoodsReceiptByIdQuery(string id) : IRequest<CommandResponse<GoodsReceiptNote>>;

    public class GetGoodsReceiptByIdQueryHandler : IRequestHandler<GetGoodsReceiptByIdQuery, CommandResponse<GoodsReceiptNote>>
    {
        private readonly IWarehouseContext _context;

        public GetGoodsReceiptByIdQueryHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse<GoodsReceiptNote>> Handle(GetGoodsReceiptByIdQuery request, CancellationToken cancellationToken)
        {
            var grn = await _context.GoodsReceiptNotes.Include(x => x.GrnItems).FirstOrDefaultAsync(x => x.Id == request.id);
            return new(grn);
        }
    }
}