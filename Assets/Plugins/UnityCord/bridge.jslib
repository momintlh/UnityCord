mergeInto(LibraryManager.library, {
    Hello: function () {
        window.alert("Hello, world!");
    },

    PatchUrlMappingsInternal: function (prefix, target) {
        prefix = UTF8ToString(prefix)
        target = UTF8ToString(target)

        console.log(`[JSLIB]: PatchingURL: ${prefix} and ${target}`)

        try {
            SDK.patchUrlMappings([{ prefix: prefix, target: target }])
        } catch (error) {
            console.error(`[JSLIB] error PatchUrlMappingsInternal: ${error}`)
        }
    },


    AttemptRemapInternal: function (urlString, prefix, target) {
        const url = new URL(UTF8ToString(urlString))
        console.log(`[JSLIB]: AttemptRemapInternal: url: ${url}, prefix: ${prefix}, target: ${target}`)
        
        try {
            SDK.attemptRemap({ url: url, mappings: [{ prefix: prefix, target: target }] })
        } catch (error) {
            console.error(`[JSLIB] error AttemptRemapInternal: ${error}`)
        }
    },
});