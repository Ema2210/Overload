

using System;

public interface IResourceEventProvider
{
    void SubscribeToEvent(Action action);
    void UnsubscribeFromEvent(Action action);
}

