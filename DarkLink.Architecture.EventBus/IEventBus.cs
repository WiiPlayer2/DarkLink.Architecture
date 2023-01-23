namespace DarkLink.Architecture.EventBus;

public interface IEventBus
{
    IObservable<object> Events { get; }

    void Publish(object @event);
}
