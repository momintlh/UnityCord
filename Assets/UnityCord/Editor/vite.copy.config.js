import { defineConfig } from 'vite';
import { fileURLToPath } from 'url';
import { dirname, resolve } from 'path';
import { viteStaticCopy } from 'vite-plugin-static-copy';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// Custom plugin to remove empty bundle output
function removeEmptyBundle() {
  return {
    name: 'remove-empty-bundle',
    generateBundle(options, bundle) {
      for (const fileName in bundle) {
        const chunk = bundle[fileName];
        // If the file is generated from empty.ts, remove it
        if (chunk.type === 'chunk' && chunk.code.trim() === '') {
          delete bundle[fileName];
        }
      }
    }
  };
}

export default defineConfig({
  plugins: [
    viteStaticCopy({
      targets: [
        {
          src: 'src/index.ts',
          dest: './',
          rename: 'bridge.jslib'
        },
      ]
    }),
    removeEmptyBundle() // Add the custom plugin
  ],
  build: {
    emptyOutDir: false,
    rollupOptions: {
      input: resolve(__dirname, 'src/empty.ts')
    },
    outDir: '../../Assets/Plugins/UnityCord'
  },
});
