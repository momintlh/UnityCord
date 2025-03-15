### UnityCord

UnityCord acts as a bridge for [Discord's Embedded App SDK](https://github.com/discord/embedded-app-sdk) and provides additional utilities to simplify creating Discord activities with Unity.

Currently, there are no official UnityCord documentation pages, but you can easily follow the [official Discord documentation](https://discord.com/developers/docs/developer-tools/embedded-app-sdk#install-the-sdk) since the differences are minimal.

<table>
<tr>
<td> Embedded-sdk-app (JS/TS) </td> <td> UnityCord (C#) </td>
</tr>
<tr>
<td>

```ts
    import {patchUrlMappings} from '@discord/embedded-app-sdk';
    patchUrlMappings([{prefix: '/foo', target: 'foo.com'}]);
```

</td>
<td>
    
```cs
using UnityCord;

List<Mapping> mappings = new List<Mapping> {
    new Mapping { Prefix = "foo", Target = "foo.com" }
};

Utils.PatchUrlMappings(mappings);
```
</td>
</tr>
</table>