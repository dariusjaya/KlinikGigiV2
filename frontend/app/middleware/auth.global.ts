export default defineNuxtRouteMiddleware((to) => {
    const authStore = useAuthStore();

    if (import.meta.client) {
        authStore.restoreSession();
    }

    if (!authStore.isLoggedIn && to.path !== '/login') {
        return navigateTo('/login');
    }
});