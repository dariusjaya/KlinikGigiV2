<script setup lang="ts">
const email = ref('');
const password = ref('');
const errorMessage = ref('');
const loading = ref(false);

const authStore = useAuthStore();

async function handleLogin() {
  errorMessage.value = '';
  loading.value = true;
  try {
    await authStore.login(email.value, password.value);
    await navigateTo('/patients');
  } catch (e: any) {
    errorMessage.value = e.message ?? 'Login gagal';
  } finally {
    loading.value = false;
  }
}
</script>

<template>
  <div class="flex min-h-screen items-center justify-center bg-gray-50">
    <UCard class="w-full max-w-sm">
      <template #header>
        <h1 class="text-xl font-semibold text-center">Login Klinik Gigi</h1>
      </template>

      <form class="space-y-4" @submit.prevent="handleLogin">
        <UFormField label="Email">
          <UInput v-model="email" type="email" placeholder="perawat@klinik.com" class="w-full" />
        </UFormField>

        <UFormField label="Password">
          <UInput v-model="password" type="password" placeholder="••••••••" class="w-full" />
        </UFormField>

        <p v-if="errorMessage" class="text-sm text-red-500">{{ errorMessage }}</p>

        <UButton type="submit" block :loading="loading">
          Masuk
        </UButton>
      </form>
    </UCard>
  </div>
</template>