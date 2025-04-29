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
        public DiscordSDK()
        {}

        public DiscordSDK(string clientId)
        {
            DiscordSDKInternal(clientId);
        }

        public void Ready(Action callback)
        {
            CallbackHandler.RegisterCallback("ready", callback);
            ReadyInternal(CallbackHandler.InvokeAction);
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void DiscordSDKInternal(string clientId);

        [DllImport("__Internal")]
        private static extern void ReadyInternal(Action<string> callback);
        #endregion
    }
}