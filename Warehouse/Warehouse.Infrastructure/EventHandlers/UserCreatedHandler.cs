using EventContracts.Event;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.EventHandlers
{
    class UserCreatedHandler : IConsumer<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;
        private readonly IWarehouseContext _context;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger,
            IWarehouseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var user = context.Message;
            await _context.Users.AddAsync(new User()
            {
                Id = user.Id,
                Name = user.FirstName
            });
            await _context.SaveChangesAsync();
            _logger.LogInformation($"{nameof(UserCreated)} event succesefully added user into OrderService");
        }
    }
}
