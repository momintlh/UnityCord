using System.Collections.Generic;
using System;
using AOT;

namespace UnityCord
{
    public class CallbackHandler
    {
        private static Dictionary<string, Delegate> callbacks = new();

        public static void RegisterCallback(string key, Delegate callback)
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

        [MonoPInvokeCallback(typeof(Action<string>))]
        public static void InvokeAction(string key)
        {
           InvokeCallback(key);
        }
    }
}