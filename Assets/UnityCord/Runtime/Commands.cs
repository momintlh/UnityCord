using System;
using System.Runtime.InteropServices;

namespace UnityCord
{
    public class Commands
    {
        public static void OpenExternalLink(string url)
        {
            OpenExternalLinkInternal(url);
        }

        public static void OpenInviteDialog(Action callback)
        {
            CallbackHandler.RegisterCallback("invite", callback);
            OpenInviteDialogInternal(CallbackHandler.InvokeAction);
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void OpenExternalLinkInternal(string url);

        [DllImport("__Internal")]
        private static extern void GetUserInternal(string id, Action<string> callback);

        [DllImport("__Internal")]
        private static extern void OpenInviteDialogInternal(Action<string> callback);
        #endregion
    }
}