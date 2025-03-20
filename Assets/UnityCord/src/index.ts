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
        {{{ makeDynCall("v", "callback") }}}(null);
      });
    } catch (error) {
      console.error(`[JSLIB] error ReadyInternal: ${error}`);
    }
  },
  //#endregion

  //#region Commands
  AuthorizeInternal : function (authorizeInput , callback) 
  {
    // TODO: Parse authorizeInput JSON.

    globals.discordSdK.commands.authorize({client_id: "1234", scope: ["activities.read"], response_type: "code" ,code_challenge: "123", state: "123", code_challenge_method: "S256", prompt:"none"}).then(result => {
      // TODO: convert result to C# string 
      {{{ makeDynCall("vi", "callback") }}}(result);
    })
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
  //#endregion
};

autoAddDeps(LibraryMyPlugin, "$globals");
mergeInto(LibraryManager.library, LibraryMyPlugin);