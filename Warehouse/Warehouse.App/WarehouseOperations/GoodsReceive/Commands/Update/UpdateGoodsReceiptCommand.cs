using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Update
{
    public record UpdateGoodsReceiptCommand(string Id, string VendorName) : IRequest<CommandResponse>;
    
    public class UpdateGoodsReceiptHandler : IRequestHandler<UpdateGoodsReceiptCommand, CommandResponse>
    {
        private readonly IWarehouseContext _context;

        public UpdateGoodsReceiptHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse> Handle(UpdateGoodsReceiptCommand request, CancellationToken cancellationToken)
        {
            var grn = await _context.GoodsReceiptNotes.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (request.VendorName != grn.VendorName)
            {
                grn.VendorName = request.VendorName;
                await _context.SaveChangesAsync(cancellationToken);
            }
            return new();
        }
    }
}