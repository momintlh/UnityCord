var LibraryMyPlugin = {
  $globals: {
    discordSdK: null,
  },

  //#region Initialization
  DiscordSDKInternal: function (clientId) {
    clientId = UTF8ToString(clientId);
    console.log(`[JSLIB]: DiscordSDKInternal: ${clientId}`);
    try {
      globals.discordSdK = new SDK.DiscordSDK(clientId);
    } catch (error) {
      console.error(`[JSLIB] error DiscordSDKInternal: ${error}`);
    }
  },
  //#endregion

  //#region Methods
  ReadyInternal: function (key, callback) {
    try {
      globals.discordSdK.ready().then(() => {
        // prettier-ignore
        {{{makeDynCall("vi", 'callback')}}}(key);
      });
    } catch (error) {
      console.error(`[JSLIB] error ReadyInternal: ${error}`);
    }
  },
  //#endregion

  //#region Commands
  OpenExternalLinkInternal: function (url, callback) {
    url = UTF8ToString(url);
    globals.discordSdK.commands
      .openExternalLink({
        url: url,
      })
      .then(({ opened }) => {
        console.log(`[JSLIB] URL ${url} opened? ${opened}`);
      });
  },

  OpenInviteDialogInternal: function(key, callback) {
    try {
      globals.discordSdK.commands.openInviteDialog().then(() => {
        {{{makeDynCall("vi", 'callback')}}}(key);
      });
    } catch (error) {
      console.error(`[JSLIB] Error in OpenInviteDialogInternal: ${error}`);
    }
  },

  GetUserInternal: function(userId, callback) {
    userId = UTF8ToString(userId);
    globals.discordSdK.commands.getUser({ id: userId }).then((result) => {
      console.log(`[JSLIB]: Result in GetUser: ${result}`);
       {{{makeDynCall("vi", 'callback')}}}(result.avatar);
    });
  },
  //#endregion

  //#region Utils
  PatchUrlMappingsInternal: function (prefix, target) {
    prefix = UTF8ToString(prefix);
    target = UTF8ToString(target);

    console.log(`[JSLIB]: PatchingURL: ${prefix} and ${target}`);

    try {
      SDK.patchUrlMappings([{ prefix: prefix, target: target }]);
    } catch (error) {
      console.error(`[JSLIB] error PatchUrlMappingsInternal: ${error}`);
    }
  },

  AttemptRemapInternal: function (urlString, prefix, target) {
    const url = new URL(UTF8ToString(urlString));
    console.log(
      `[JSLIB]: AttemptRemapInternal: url: ${url}, prefix: ${prefix}, target: ${target}`
    );

    try {
      SDK.attemptRemap({
        url: url,
        mappings: [{ prefix: prefix, target: target }],
      });
    } catch (error) {
      console.error(`[JSLIB] error AttemptRemapInternal: ${error}`);
    }
  },

  // Convert to C# string
  ConvertString: function (str) {
    var bufferSize = lengthBytesUTF8(str) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(str, buffer, bufferSize);
    return buffer;
  },

  //#endregion
};

autoAddDeps(LibraryMyPlugin, "$globals");
mergeInto(LibraryManager.library, LibraryMyPlugin);
