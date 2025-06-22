using System;
using System.Collections.Generic;

public static class EventBus {
    private static Dictionary<Type, Delegate> eventTable = new();

    public static void Subscribe<T>(Action<T> callback) {
        var type = typeof(T);
        if (eventTable.ContainsKey(type)) {
            eventTable[type] = Delegate.Combine(eventTable[type], callback);
        }
        else {
            eventTable[type] = callback;
        }
    }

    public static void Unsubscribe<T>(Action<T> callback) {
        var type = typeof(T);
        if (!eventTable.ContainsKey(type)) return;

        var currentDel = Delegate.Remove(eventTable[type], callback);
        if (currentDel == null) {
            eventTable.Remove(type);
        }
        else {
            eventTable[type] = currentDel;
        }
    }

    public static void Publish<T>(T publishedEvent) {
        if (eventTable.TryGetValue(typeof(T), out var del)) {
            (del as Action<T>)?.Invoke(publishedEvent);
        }
    }
}
