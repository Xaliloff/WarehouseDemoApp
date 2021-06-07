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

namespace Warehouse.App.Products.Queries
{
    public class GetProductsQuery : IRequest<List<Product>>
    {
        public string Name { get; set; }
    }

    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IWarehouseContext _context;

        public GetProductsHandler(IWarehouseContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.Where(x => x.Name.Contains(request.Name)).ToListAsync();
        }
    }
}
