using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseItems.Queries
{
    public class GetItemsQuery : IRequest<List<Item>>
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Condition { get; set; }
    }

    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<Item>>
    {
        private readonly IWarehouseContext _context;

        public GetItemsHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Items.AsQueryable();
            if (!string.IsNullOrEmpty(request.ProductId))
            {
                query = query.Where(x => x.ProductId == request.ProductId);
            }

            if (!string.IsNullOrEmpty(request.ProductName) && string.IsNullOrEmpty(request.ProductId))
            {
                query = query.Where(x => x.Product.Name.Contains(request.ProductName));
            }

            if (!string.IsNullOrEmpty(request.Condition))
            {
                query = query.Where(x => x.Condition.ToString() == request.Condition);
            }

            return await query.ToListAsync();
        }
    }
}