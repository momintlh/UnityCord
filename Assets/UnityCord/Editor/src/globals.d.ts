import { DiscordSDK } from "@discord/embedded-app-sdk";

export {}; // Ensure this file is treated as a module

declare global {
  // Augment the Window interface so that the SDK property is recognized globally.
  interface Window {
    SDK: typeof import("@discord/embedded-app-sdk");
  }

  // Optionally, if you prefer using the SDK identifier directly, you can declare it:
  const SDK: typeof import("@discord/embedded-app-sdk");

  const globals: {
    discordSdK: DiscordSDK;
  };

  // Other global declarations for your jslib
  function UTF8ToString(ptr: number): string;
  // JS to C#
  function stringToUTF8(str: string, buffer: number, bufferSize: number);

  function lengthBytesUTF8(str: string): number
  function _malloc(bytes: number)
  function ConvertString(str: string): number
  function _ConvertString(str: string): number


  const LibraryManager: {
    library: any;
  };

  // {{{ makeDynCall('v', 'callback') }}}()
  type DynCallSignature = "v" | "vi" | "vii";
  function makeDynCall(
    sig: DynCallSignature,
    ptr: any
  ): (...args: any[]) => any;
  function autoAddDeps(obj: any, key: string): void;
  function mergeInto(target: any, obj: any): void;
}