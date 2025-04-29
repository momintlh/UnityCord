using System.Collections.Generic;
using System;

public class CallbackHandler
{
    private static Dictionary<string, Delegate> callbacks = new();

    public static void RegisterCallback(string key, Action callback)
    {
        callbacks.TryAdd(key, callback);
    }

    public static void InvokeCallback(string key)
    {
        if (callbacks.TryGetValue(key, out Delegate callback))
        {
            (callback as Action).Invoke();
        }
    }
}