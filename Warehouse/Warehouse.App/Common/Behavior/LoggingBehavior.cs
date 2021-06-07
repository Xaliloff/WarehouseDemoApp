using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Services;

namespace Warehouse.App.Common.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            ICurrentUserService service)
        {
            _logger = logger;
            _currentUserService = service;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userId = _currentUserService.UserId;
            var userName = "";
            var requestName = request.GetType().Name;
            _logger.LogInformation($"{userName} (id: {userId}) initiates {requestName} command.");

            var response = await next();
            return response;
        }
    }
}