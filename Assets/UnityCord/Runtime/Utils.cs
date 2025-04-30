using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenQA.Selenium.DevTools;
using UnityEngine;

namespace UnityCord
{
    // TODO: rename to DiscordUtils or DiscordHelpers
    public class Utils
    {
        public static bool IsDicordContext()
        {   
            return Application.absoluteURL.Contains("discord");
        }

        internal static bool ValidateDiscord(string warningMessage)
        {
            if (!IsDicordContext())
            {
                UnityEngine.Debug.LogWarning($"[DiscordSDK] {warningMessage}");
                return false;
            }
            return true;
        }

        public static void PatchUrlMappings(List<Mapping> mappings, PatchUrlMappingsConfig config = null)
        {
            if (!ValidateDiscord("PatchUrlMappings only works inside discord."))
                return;

            for (int i = 0; i < mappings.Count; i++)
            {
                // TODO: Pass the config:
                PatchUrlMappingsInternal(mappings[i].Prefix, mappings[i].Target);
            }
        }

        public static void AttemptRemap(RemapInput remapInput)
        {
            if (!ValidateDiscord("AttemptRemap only works inside discord."))
                return;

            for (int i = 0; i < remapInput.Mappings.Count; i++)
            {
                AttemptRemapInternal(remapInput.URL, remapInput.Mappings[i].Prefix, remapInput.Mappings[i].Target);
            }
        }


        #region Internals

        [DllImport("__Internal")]
        // TODO: Pass the config:
        private static extern void PatchUrlMappingsInternal(string prefix, string target);

        [DllImport("__Internal")]
        private static extern void AttemptRemapInternal(string URL, string prefix, string target);

        #endregion
    }
}
