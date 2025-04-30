using System.Collections.Generic;
using System;
using AOT;
using UnityEngine;

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

        public static Delegate GetCallback(string key)
        {
            if (callbacks.TryGetValue(key, out Delegate callback))
            {
                return callback;
            }
            return null;
        }



        [MonoPInvokeCallback(typeof(Action<string>))]
        public static void InvokeAction(string key)
        {
            Debug.LogWarning($"[Unity]: key is {key}");
            InvokeCallback(key);
        }
    }
}