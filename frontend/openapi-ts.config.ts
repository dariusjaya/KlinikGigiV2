import { createClient } from '@hey-api/openapi-ts';

createClient({
    input: '../openapi.json',
    output: './app/client',
    plugins: ['@hey-api/typescript', '@hey-api/sdk'],
});