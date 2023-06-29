using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace EducationalPlatform.Application.Abstractions;

public abstract class DomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : DomainEvent
{
    private readonly ILogger<DomainEventHandler<TEvent>> _logger;

    protected DomainEventHandler(ILogger<DomainEventHandler<TEvent>> logger)
    {
        _logger = logger;
    }

    public Task Handle(TEvent domainEvent, CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            var result = await Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(30))
                .ExecuteAndCaptureAsync(async () => await Handle(domainEvent));

            if (result.Outcome == OutcomeType.Successful)
                _logger.LogInformation(@"{Handler} executed successfully with object: {Object}",
                    GetType().Name,
                    domainEvent);
            else
                _logger.LogError(
                    @"{Handler} executed unsuccessfully with object: {Object}. Exception Message: {ExceptionMessage}",
                    GetType().Name,
                    domainEvent,
                    result.FinalException.Message);
        }, cancellationToken);

        return Task.CompletedTask;
    }

    protected abstract Task Handle(TEvent domainEvent);
}