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
                // TODO: Pass the config:
                PatchUrlMappingsInternal(mappings[i].Prefix, mappings[i].Target);
            }
        }

        public static void AttemptRemap(RemapInput remapInput)
        {
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
