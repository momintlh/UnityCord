import { defineConfig } from 'vite';
import { resolve } from 'path';
import { viteStaticCopy } from 'vite-plugin-static-copy';

export default defineConfig({
    define: {
        "process.env": { NODE_ENV: "production" },
    },
    plugins: [
        viteStaticCopy({
            targets: [
                {
                    src: 'src/index.ts',
                    dest: './',
                    rename: 'bridge.jslib'
                },
            ]
        })
    ],
    build: {
        lib: {
            name: "UnityCordPluginFrameworks",
            entry: resolve(__dirname, 'src/frameworks.ts'),
            fileName: (format) => `UnityCordFrameworks.jspre`,
            formats: ['umd']
        },
        outDir: '../../Plugins/UnityCord',
        minify: false,
        rollupOptions: {}
    },
});