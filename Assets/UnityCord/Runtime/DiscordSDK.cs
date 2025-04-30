using System;
using System.Runtime.InteropServices;

namespace UnityCord
{
    /// <summary>
    /// This will be the entry point for interacting with Discord SDK. 
    /// </summary>
    public class DiscordSDK
    {
        public Commands commands;

        public DiscordSDK()
        {
            commands = new();
        }

        public DiscordSDK(string clientId)
        {
            commands = new();
            if (!Utils.ValidateDiscord("DiscordSDK only works inside discord, mocking is currently not supported")) return;
            DiscordSDKInternal(clientId);
        }

        public void Ready(Action callback)
        {
            if (!Utils.ValidateDiscord("`Ready` only works inside discord")) return;

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