using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.App.WarehouseTransactions.GoodsIssue.Commands.UpdateGoodsIssueNote
{
    public class UpdateGoodsIssueCommand : IRequest<CommandResponse>, IValidateable
    {
        public string Id { get; set; }
        public List<Good> Goods { get; set; }

        public class Good
        {
            public string Id { get; set; }
            public string ItemId { get; set; }
        }
    }

    public class UpdateGoodsIssueNoteHandler : IRequestHandler<UpdateGoodsIssueCommand, CommandResponse>
    {
        private readonly IWarehouseContext _context;

        public UpdateGoodsIssueNoteHandler(IWarehouseContext context)
        {
            _context = context;
        }

        public async Task<CommandResponse> Handle(UpdateGoodsIssueCommand request, CancellationToken cancellationToken)
        {
            var existingGin = await _context.GoodsIssueNotes.Include(x => x.GinItems).FirstAsync(x => x.Id == request.Id);
            
            //non existing gin items should be deleted
            //existing ones should be updated
            existingGin.GinItems.ForEach(ginItem =>
            {
                if (!request.Goods.Any(g => g.Id == ginItem.Id))
                {
                    _context.GoodsIssueNoteItems.Remove(ginItem);
                }
                else if (request.Goods.Any(g => g.Id == ginItem.Id))
                {
                    ginItem.ItemId = request.Goods.First(x => x.Id == ginItem.Id).ItemId;
                    _context.GoodsIssueNoteItems.Update(ginItem);
                }
            });
            //and new ones should be added into context
            _context.GoodsIssueNoteItems.AddRange(request.Goods
                                .Where(x => !existingGin.GinItems.Any(i => i.Id == x.Id))
                                .Select(x => new GoodsIssueNoteItem()
                                {
                                    GoodIssueNoteId = existingGin.Id,
                                    ItemId = x.ItemId
                                }));

            await _context.SaveChangesAsync(cancellationToken);
            return new();
        }
    }
}