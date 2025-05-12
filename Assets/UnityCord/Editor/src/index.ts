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
  ReadyInternal: function (callback) {
    try {
      globals.discordSdK.ready().then(() => {
        {{{ makeDynCall("v", "callback") }}}();
      });
    } catch (error) {
      console.error(`[JSLIB] error ReadyInternal: ${error}`);
    }
  },
  //#endregion

  //#region Commands
  StartPurchaseInternal: function (skuId, callback) {
    try {
      skuId = UTF8ToString(skuId);
      globals.discordSdK.commands
        .startPurchase(skuId)
        .then((result) => {
          console.log(`[JSLIB] StartPurchaseInternal result: ${result}`);
          var dataJson = JSON.stringify(result);
          var data = _ConvertString(dataJson);
          var key = _ConvertString(skuId);

          {{{ makeDynCall("vi", "callback") }}}(key, data);
        })
        .catch((err) => {
          console.error("[JSLIB] StartPurchaseInternal error:", err);
        });
    } catch (error) {
      console.error(`[JSLIB] Error in StartPurchaseInternal: ${error}`);
    }
  },

  OpenExternalLinkInternal: function (url) {
    url = UTF8ToString(url);
    globals.discordSdK.commands
      .openExternalLink({
        url: url,
      })
      .then(({ opened }) => {
        console.log(`[JSLIB] URL ${url} opened? ${opened}`);
      });
  },

  OpenInviteDialogInternal: function() {
    try {
      globals.discordSdK.commands.openInviteDialog().then(() => {
        console.log("[JSLIB] invite dialog opened")
      }).catch(err => {
        console.error("[JSLIB] OpenInviteDialogInternal error:", err);
      });
    } catch (error) {
      console.error(`[JSLIB] Error in OpenInviteDialogInternal: ${error}`);
    }
  },
  
  GetUserInternal: function(id, callback) {
    try {
      id = UTF8ToString(id);
      globals.discordSdK.commands.getUser(id).then((result) => {
        console.log(`[JSLIB] GetUserInternal result: ${result}`);
        var dataJson = JSON.stringify(result);
        var data = _ConvertString(dataJson);
        var key = _ConvertString(id);

        {{{ makeDynCall("vi", "callback") }}}(key, data);

      }).catch(err => {
        console.error("[JSLIB] GetUserInternal error:", err);
      });
    } catch (error) {
      console.error(`[JSLIB] Error in GetUserInternal: ${error}`);
    }
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
