using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Warehouse.App.WarehouseOperations.GoodsReceive.Queries.Get;
using Warehouse.App.WarehouseOperations.GoodsReceive.Queries.GetById;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Approve;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Create;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Update;

namespace Warehouse.API.Controllers
{
    [Route("api/goodsReceipt")]
    [ApiController]
    public class GoodsReceiptController : ApplicationController
    {
        private readonly IMediator _mediator;

        public GoodsReceiptController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetById()
        {
            return HandleRequest(await _mediator.Send(new GetGoodsReceiptQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return HandleRequest(await _mediator.Send(new GetGoodsReceiptByIdQuery(id)));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateGoodsReceiptCommand command)
        {
            var a = await _mediator.Send(command);
            return HandleRequest(a);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateGoodsReceiptCommand command)
        {
            return HandleRequest(await _mediator.Send(command));
        }

        [HttpPost("approve")]
        public async Task<ActionResult> Approve(ApproveGoodsReceiptCommand command)
        {
            return HandleRequest(await _mediator.Send(command));
        }
    }
}