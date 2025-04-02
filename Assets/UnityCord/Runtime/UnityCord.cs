using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

namespace UnityCord
{
    /// <summary>
    /// This will be the entry point for interacting with Discord SDK. 
    /// </summary>
    public class DiscordSDK
    {
        private static Dictionary<string, Delegate> Callbacks = new();

        public DiscordSDK()
        {
        }

        public DiscordSDK(string clientId)
        {
            DiscordSDKInternal(clientId);
        }


        public void Ready(Action callback)
        {
            Callbacks.Add("ready", callback);
            ReadyInternal(InvokeCallback);
        }

        // TODO: make a proper callback handler
        [MonoPInvokeCallback(typeof(Action))]
        private static void InvokeCallback()
        {
            if (Callbacks.TryGetValue("ready", out Delegate del))
            {
                (del as Action)?.Invoke();
            }
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void DiscordSDKInternal(string clientId);

        [DllImport("__Internal")]
        private static extern void ReadyInternal(Action callback);
        #endregion
    }
}