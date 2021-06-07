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
    public record GetProductByIdQuery(string ProductId) : IRequest<Product>;

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IWarehouseContext _context;

        public GetProductByIdHandler(IWarehouseContext context)
        {
            _context = context;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
        }
    }
}
