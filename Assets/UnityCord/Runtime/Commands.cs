using System;
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
        
        public static void GetUser(string userId, Action callback)
        {
            GetUserInternal(userId, callback);
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void OpenExternalLinkInternal(string url);

        [DllImport("__Internal")]

        private static extern void GetUserInternal(string id, Action callback);
        #endregion
    }
}