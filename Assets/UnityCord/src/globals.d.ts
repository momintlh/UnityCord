export {}; // Ensure this file is treated as a module

declare global {
  // Augment the Window interface so that the SDK property is recognized globally.
  interface Window {
    SDK: typeof import("@discord/embedded-app-sdk");
  }

  // Optionally, if you prefer using the SDK identifier directly, you can declare it:
  const SDK: typeof import("@discord/embedded-app-sdk");

  // Other global declarations for your jslib
  function UTF8ToString(ptr: number): string;

  const LibraryManager: {
    library: any;
  };

  function mergeInto(target: any, obj: any): void;
}
