using System;
using System.Runtime.InteropServices;
using NSubstitute;

namespace UnityCord
{
    public class Commands
    {
        public void OpenExternalLink(string url)
        {
            if (!Utils.ValidateDiscord("OpenExternalLink only works inside dicord")) return;

            OpenExternalLinkInternal(url);
        }

        public void OpenInviteDialog()
        {
            if (!Utils.ValidateDiscord("OpenInviteDialog only works inside dicord")) return;

            OpenInviteDialogInternal();
        }

        public void GetUser(string id, Action<string> callback)
        {
            if (!Utils.ValidateDiscord("GetUser only works inside discord")) return;

            CallbackHandler.RegisterCallback(id, callback);

            GetUserInternal(id, CallbackHandler.InvokeAction);
        }

        #region Internals
        [DllImport("__Internal")]
        private static extern void OpenExternalLinkInternal(string url);

        [DllImport("__Internal")]
        private static extern void OpenInviteDialogInternal();

        [DllImport("__Internal")]
        private static extern void GetUserInternal(string id, Action<string, string> callback);
        #endregion
    }
}