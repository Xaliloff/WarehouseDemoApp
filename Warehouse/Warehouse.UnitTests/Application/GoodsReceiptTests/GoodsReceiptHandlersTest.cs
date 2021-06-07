using EventContracts.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.App.Services;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Approve;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Create;
using Warehouse.App.WarehouseTransactions.GoodsReceive.Commands.Update;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Infrastructure.Context;
using Xunit;

namespace Warehouse.UnitTests.Application.GoodsReceiptTests
{
    public class GoodsReceiptHandlersTest
    {
        private readonly DbContextOptions<WarehouseContext> _dbOptions;
        private readonly Mock<ICurrentUserService> currentUserService;

        public GoodsReceiptHandlersTest()
        {
            _dbOptions = new DbContextOptionsBuilder<WarehouseContext>()
                .UseInMemoryDatabase(databaseName: "in-memory")
                .Options;
            currentUserService = new Mock<ICurrentUserService>();
        }

        [Fact]
        public async Task Handle_persisted_grn_successfully()
        {
            var cltToken = new System.Threading.CancellationToken();
            var context = new WarehouseContext(_dbOptions, currentUserService.Object);
            var handler = new CreateGoodsReceiptHandler(context);
            var request = new CreateGoodsReceiptCommand()
            {
                Goods = new()
                {
                    new() { ProductId = "productId123", Quantity = 5 },
                    new() { ProductId = "productId1234", Quantity = 20 },
                    new() { ProductId = "productId12345", Quantity = 10 },
                },
                VendorName = "VendorTest1"
            };

            var result = await handler.Handle(request, cltToken);

            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Grn_succesefully_updated()
        {
            //arrange
            var cltToken = new System.Threading.CancellationToken();
            var context = new WarehouseContext(_dbOptions, currentUserService.Object);
            var existingGrn = await CreateGrn(context);

            var handler = new UpdateGoodsReceiptHandler(context);
            var request = new UpdateGoodsReceiptCommand(existingGrn.Id, existingGrn.VendorName + "changed");

            //act
            var result = await handler.Handle(request, cltToken);

            //assert
            Assert.False(result.HasErrors);
        }

        [Fact]
        public async Task Grn_successfully_approved()
        {
            //arrange
            var cltToken = new System.Threading.CancellationToken();
            var context = new WarehouseContext(_dbOptions, currentUserService.Object);

            var busMock = new Mock<IPublishEndpoint>();

            var existingGrn = await CreateGrn(context);

            var handler = new ApproveGoodsReceiptHandler(context, busMock.Object);
            var request = new ApproveGoodsReceiptCommand(existingGrn.Id);

            //act
            var result = await handler.Handle(request, cltToken);

            //assert
            Assert.False(result.HasErrors);
            Assert.Equal(DocumentState.Approved, existingGrn.DocumentState);
        }

        private async Task<GoodsReceiptNote> CreateGrn(WarehouseContext context, bool detach = false)
        {
            var existingGrn = new GoodsReceiptNote() { VendorName = "VendorName", DocumentState = DocumentState.Draft };
            existingGrn.GrnItems.AddRange(new List<GoodsReceiptNoteItem>()
            {
                new() { ProductId = Guid.NewGuid().ToString(), Quantity = 5 },
                new() { ProductId = Guid.NewGuid().ToString(), Quantity = 20 },
                new() { ProductId = Guid.NewGuid().ToString(), Quantity = 10 },
            });
            await context.GoodsReceiptNotes.AddAsync(existingGrn);
            await context.SaveChangesAsync();

            if (detach)
            {
                context.Entry(existingGrn).State = EntityState.Detached;
                existingGrn.GrnItems.ForEach(x => context.Entry(x).State = EntityState.Detached);
            }

            return existingGrn;
        }
    }
}