mergeInto(LibraryManager.library, {
    Hello: function () {
        window.alert("Hello, world!");
        const discordSdk = new SDK.DiscordSDKMock(DISCORD_CLIENT_ID);
    },
});