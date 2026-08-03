import { client } from '~/client/client.gen';

export default defineNuxtPlugin(() => {
    client.setConfig({
        baseUrl: 'https://localhost:57679',
    });

    client.interceptors.request.use((request) => {
        const authStore = useAuthStore();
        if (authStore.token) {
            request.headers.set('Authorization', `Bearer ${authStore.token}`);
        }
        return request;
    });
});