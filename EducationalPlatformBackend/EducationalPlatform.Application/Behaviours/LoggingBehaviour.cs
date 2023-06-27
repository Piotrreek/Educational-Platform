using MediatR;
using Microsoft.Extensions.Logging;

namespace EducationalPlatform.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {@RequestName} with {@Values}", typeof(TRequest).Name, request);

        var response = await next();

        _logger.LogInformation("Handled {@RequestName}", typeof(TRequest).Name);

        return response;
    }
}