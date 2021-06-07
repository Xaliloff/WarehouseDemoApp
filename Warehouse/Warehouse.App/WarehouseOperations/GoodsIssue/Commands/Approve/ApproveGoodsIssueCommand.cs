using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Warehouse.App.WarehouseTransactions.GoodsIssue.Commands.ApproveGoodsIssueNote
{
    public record ApproveGoodsIssueCommand : IRequest<CommandResponse>;

    public class ApproveGoodsIssueHandler : IRequestHandler<ApproveGoodsIssueCommand, CommandResponse>
    {
        public Task<CommandResponse> Handle(ApproveGoodsIssueCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}