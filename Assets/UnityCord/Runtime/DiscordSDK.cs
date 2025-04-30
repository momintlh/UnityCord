using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AOT;
using OpenQA.Selenium.DevTools.V85.IndexedDB;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace UnityCord
{
    /// <summary>
    /// This will be the entry point for interacting with Discord SDK. 
    /// </summary>
    public class DiscordSDK
    {
        private static Dictionary<string, Action> Callbacks = new();

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
            if (!Utils.ValidateDiscord("(Ready) DiscordSDK only works inside discord")) return;

            Callbacks["ready"] = callback;

            ReadyInternal(InvokeReadyCallback);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void InvokeReadyCallback()
        {
            if (Callbacks.TryGetValue("ready", out Action action))
            {
                action?.Invoke();
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