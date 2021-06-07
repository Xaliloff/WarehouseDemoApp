using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.Transactions.Queries
{
    public class GetTransactionsQuery : IRequest<List<WarehouseTransaction>>
    {
        public string Type { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
    }

    public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<WarehouseTransaction>>
    {
        private readonly IWarehouseContext _context;

        public GetTransactionsHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseTransaction>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.WarehouseTransactions.AsQueryable();
            if (!string.IsNullOrEmpty(request.ProductId))
            {
                query = query.Where(x => x.ProductId == request.ProductId);
            }

            if (!string.IsNullOrEmpty(request.ProductName) && string.IsNullOrEmpty(request.ProductId))
            {
                query = query.Where(x => x.Product.Name.Contains(request.ProductName));
            }

            if (!string.IsNullOrEmpty(request.Type))
            {
                query = query.Where(x => x.TransactionType == request.Type);
            }

            return await query.ToListAsync();
        }
    }
}