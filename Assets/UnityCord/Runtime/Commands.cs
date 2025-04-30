using System;
using System.Runtime.InteropServices;

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

        #region Internals
        [DllImport("__Internal")]
        private static extern void OpenExternalLinkInternal(string url);

        [DllImport("__Internal")]
        private static extern void OpenInviteDialogInternal();
        #endregion
    }
}