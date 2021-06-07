using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Warehouse.App.Common.Interfaces;
using Warehouse.App.Services;

namespace Warehouse.App.Common.Behavior
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
    where TRequest : IValidateable
    {
        private readonly IValidator<TRequest> _compositeValidator;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUser;

        public ValidationBehaviour(IValidator<TRequest> compositeValidator, ILogger<TRequest> logger, ICurrentUserService currentUser)
        {
            _compositeValidator = compositeValidator;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await _compositeValidator.ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                IEnumerable<CommandError> errors = result.Errors.Select(x => new CommandError(x.ErrorMessage));
                //_logger.LogError(EventIDs.EventIdPipelineThrown,
                //    MessageTemplates.ValidationErrorsLog,
                //    result.Errors.Select(s => s.ErrorMessage).Aggregate((acc, curr) => acc += string.Concat("_|_", curr)),
                //    _currentUser.UserName
                //    );

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(CommandResponse<>).MakeGenericType(resultType);

                    var invalidResponse =
                        Activator.CreateInstance(invalidResponseType, errors) as TResponse;
                    return invalidResponse;
                }
                else
                {
                    var invalidResp = new CommandResponse(errors) as TResponse;
                    return invalidResp;
                }
            }

            var response = await next();

            return response;
        }
    }
}