mergeInto(LibraryManager.library, {
    Hello: function () {
        window.alert("Hello, world!");
    },

    PatchUrlMappingsInternal: function (prefix, target) {
        prefix = UTF8ToString(prefix)
        target = UTF8ToString(target)

        console.log(`PatchingURL: ${prefix} and ${target}`)

        SDK.patchUrlMappings([{ prefix: prefix, target: target }])
    },
});