using System.Runtime.InteropServices;

namespace UnityCord
{
    public class Commands
    {
        // TODO: change this to non static
        public static void OpenExternalLink(string url)
        {
            OpenExternalLinkInternal(url);
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void OpenExternalLinkInternal(string url);
        #endregion
    }
}