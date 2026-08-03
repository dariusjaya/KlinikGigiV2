import { defineStore } from 'pinia';
import { klinikGigiV2WebAuthLoginLogin } from '~/client/sdk.gen';
import type { UserRecord } from '~/client/types.gen';

export const useAuthStore = defineStore('auth', {
    state: () => ({
        token: null as string | null,
        user: null as UserRecord | null,
    }),

    getters: {
        isLoggedIn: (state) => !!state.token,
    },

    actions: {
        async login(email: string, password: string) {
            const { data, error } = await klinikGigiV2WebAuthLoginLogin({
                body: { email, password },
            });

            if (error || !data?.token) {
                throw new Error(data?.message ?? 'Login gagal');
            }

            this.token = data.token;
            this.user = data.user ?? null;

            // Simpan ke localStorage supaya tidak hilang saat refresh
            if (import.meta.client) {
                localStorage.setItem('auth_token', data.token);
                localStorage.setItem('auth_user', JSON.stringify(data.user));
            }
        },

        logout() {
            this.token = null;
            this.user = null;
            if (import.meta.client) {
                localStorage.removeItem('auth_token');
                localStorage.removeItem('auth_user');
            }
            navigateTo('/login');
        },

        restoreSession() {
            if (import.meta.client) {
                const token = localStorage.getItem('auth_token');
                const user = localStorage.getItem('auth_user');
                if (token) {
                    this.token = token;
                    this.user = user ? JSON.parse(user) : null;
                }
            }
        },
    },
});