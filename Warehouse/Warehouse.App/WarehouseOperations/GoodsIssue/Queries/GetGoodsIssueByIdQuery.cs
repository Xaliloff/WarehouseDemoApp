using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseOperations.GoodsIssue.Queries
{
    public record GetGoodsIssueByIdQuery(string Id) : IRequest<GoodsIssueNote>;

    public class GetGoodsIssueByIdHandler : IRequestHandler<GetGoodsIssueByIdQuery, GoodsIssueNote>
    {
        private readonly IWarehouseContext _context;

        public GetGoodsIssueByIdHandler(IWarehouseContext context)
        {
            _context = context;
        }
        public async Task<GoodsIssueNote> Handle(GetGoodsIssueByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.GoodsIssueNotes.Include(x => x.GinItems).FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
