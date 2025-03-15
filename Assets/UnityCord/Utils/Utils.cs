using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityCord
{
    public class Utils
    {
        public static void PatchUrlMappings(List<Mapping> mappings, PatchUrlMappingsConfig config = null)
        {
            for (int i = 0; i < mappings.Count; i++)
            {
                PatchUrlMappingsInternal(mappings[i].Prefix, mappings[i].Target);
            }
        }

        [DllImport("__Internal")]
        private static extern void PatchUrlMappingsInternal(string prefix, string target);
        //bool patchFetch = true, bool patchWebSocket = true, bool patchXhr = true, bool patchSrcAttributes = false);
    }
}
