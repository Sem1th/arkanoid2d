using System;
using System.Collections.Generic;

public class EventBus : IEventBus
{
    private readonly Dictionary<Type, Delegate> _subscriptions = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> listener)
    {
        var type = typeof(T);
        if (_subscriptions.TryGetValue(type, out var existing))
        {
            _subscriptions[type] = Delegate.Combine(existing, listener);
        }
        else
        {
            _subscriptions[type] = listener;
        }
    }

    public void Unsubscribe<T>(Action<T> listener)
    {
        var type = typeof(T);
        if (_subscriptions.TryGetValue(type, out var existing))
        {
            var current = Delegate.Remove(existing, listener);
            if (current == null)
                _subscriptions.Remove(type);
            else
                _subscriptions[type] = current;
        }
    }

    public void Publish<T>(T evt)
    {
        var type = typeof(T);
        if (_subscriptions.TryGetValue(type, out var d))
        {
            var callback = d as Action<T>;
            callback?.Invoke(evt);
        }
    }
}

