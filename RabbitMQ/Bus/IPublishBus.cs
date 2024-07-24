using MassTransit;

namespace RabbitMQ.Bus;

internal interface IPublishBus
{
    Task PublicAsync<T>(T message, CancellationToken ct = default) where T : class;
}

internal class PublishBus : IPublishBus
{
    private readonly IPublishEndpoint _busEndpoint;

    public PublishBus(IPublishEndpoint busEndpoint)
    {
        _busEndpoint = busEndpoint;
    }

    public Task PublicAsync<T>(T message, CancellationToken ct = default) where T : class
    {
        return _busEndpoint.Publish(message, ct);
    }
}
