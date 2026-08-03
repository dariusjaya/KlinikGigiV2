// import { createClient } from '@hey-api/openapi-ts';

// createClient({
//   input: '../openapi.json',
//   output: './app/client',
//   plugins: [
//     {
//       name: '@hey-api/client-ofetch',
//       runtimeConfigPath: './client.config',
//     },
//     '@pinia/colada',
//   ],
// });
import { createClient } from '@hey-api/openapi-ts';

createClient({
  input: 'https://localhost:57679/openapi/v1.json',
  output: './app/client',
  plugins: ['@hey-api/typescript', '@hey-api/sdk'],
});