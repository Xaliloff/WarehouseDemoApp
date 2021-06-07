using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.App.WarehouseTransactions.GoodsIssue.Commands.CreateGoodsIssueNote
{
    public class CreateGoodsIssueCommand : IRequest<CommandResponse>, IValidateable
    {
        public CreateGoodsIssueCommand()
        {
            ItemIds = new List<string>();
        }
        public List<string> ItemIds { get; set; }
    }

    public class CreateGoodsIssueNoteHandler : IRequestHandler<CreateGoodsIssueCommand, CommandResponse>
    {
        private readonly IWarehouseContext _context;

        public CreateGoodsIssueNoteHandler(IWarehouseContext context)
        {
            _context = context;
        }
        public async Task<CommandResponse> Handle(CreateGoodsIssueCommand request, CancellationToken cancellationToken)
        {
            var gin = new GoodsIssueNote()
            {
                DocumentState = DocumentState.Draft,
                GinItems = request.ItemIds.Select(x => new GoodsIssueNoteItem() { ItemId = x }).ToList()
            };
            await _context.GoodsIssueNotes.AddAsync(gin, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new();
        }
    }
}