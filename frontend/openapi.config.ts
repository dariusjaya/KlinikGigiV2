import { createClient } from '@hey-api/openapi-ts';

createClient({
  input: '../openapi.json',
  output: './app/client',
  plugins: [
    {
      name: '@hey-api/client-ofetch',
      runtimeConfigPath: './client.config',
    },
    '@pinia/colada',
  ],
});
