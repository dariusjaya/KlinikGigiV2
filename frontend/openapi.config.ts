import { defineConfig } from '@hey-api/openapi-ts';

export default defineConfig({
    input: '../openapi.json',       // dari backend .NET
    output: {
        path: './app/client',         // hasil generate
        format: 'prettier',
    },
    plugins: [
        '@hey-api/client-fetch',
        '@tanstack/vue-query',        // atau '@pinia/colada' seperti dvend
    ],
});