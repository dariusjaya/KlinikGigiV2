```vue
<script setup lang="ts">
const authStore = useAuthStore();

const navItems = [
  { label: 'Data Pasien', to: '/patients', icon: 'i-lucide-users' },
];

const route = useRoute();
</script>

<template>
  <div class="h-screen flex bg-gray-50 overflow-hidden">
    <!-- Sidebar -->
    <aside class="w-64 h-screen shrink-0 border-r bg-white flex flex-col">
      <!-- Header -->
      <div class="px-6 py-5 border-b shrink-0">
        <h1 class="font-semibold text-lg">
          🦷 Klinik Gigi
        </h1>
      </div>

      <!-- Navigation -->
      <nav class="flex-1 min-h-0 overflow-y-auto px-3 py-4 space-y-1">
        <NuxtLink
          v-for="item in navItems"
          :key="item.to"
          :to="item.to"
          class="flex items-center gap-3 px-3 py-2 rounded-md text-sm font-medium transition-colors"
          :class="
            route.path.startsWith(item.to)
              ? 'bg-primary-50 text-primary-600'
              : 'text-gray-600 hover:bg-gray-100'
          "
        >
          <UIcon :name="item.icon" class="w-5 h-5" />
          {{ item.label }}
        </NuxtLink>
      </nav>

      <!-- Footer -->
      <div class="px-3 py-4 border-t shrink-0">
        <div class="px-3 py-2 mb-2">
          <p class="text-sm font-medium">
            {{ authStore.user?.fullName }}
          </p>

          <p class="text-xs text-gray-400">
            {{ authStore.user?.role }}
          </p>
        </div>

        <UButton
          color="neutral"
          variant="ghost"
          block
          icon="i-lucide-log-out"
          @click="authStore.logout()"
        >
          Keluar
        </UButton>
      </div>
    </aside>

    <!-- Main content -->
    <main class="flex-1 min-w-0 min-h-0 h-screen overflow-y-auto p-6">
      <slot />
    </main>
  </div>
</template>
