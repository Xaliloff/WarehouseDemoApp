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
    public record GetGoodsIssuesQuery() : IRequest<List<GoodsIssueNote>>;

    public class GetGoodsIssuesHandler : IRequestHandler<GetGoodsIssuesQuery, List<GoodsIssueNote>>
    {
        private readonly IWarehouseContext _context;

        public GetGoodsIssuesHandler(IWarehouseContext context)
        {
            _context = context;
        }
        public async Task<List<GoodsIssueNote>> Handle(GetGoodsIssuesQuery request, CancellationToken cancellationToken)
        {
            return await _context.GoodsIssueNotes.ToListAsync();
        }
    }
}
