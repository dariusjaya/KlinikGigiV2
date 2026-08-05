<script setup lang="ts">
const authStore = useAuthStore();

const navItems = [
  { label: 'Data Pasien', to: '/patients', icon: 'i-lucide-users' },
  // tambah menu lain di sini nanti, misal:
  // { label: 'Jadwal', to: '/appointments', icon: 'i-lucide-calendar' },
];

const route = useRoute();
</script>

<template>
  <div class="min-h-screen flex bg-gray-50">
    <!-- Sidebar -->
    <aside class="w-64 border-r bg-white flex flex-col">
      <div class="px-6 py-5 border-b">
        <h1 class="font-semibold text-lg">🦷 Klinik Gigi</h1>
      </div>

      <nav class="flex-1 px-3 py-4 space-y-1">
        <NuxtLink
          v-for="item in navItems"
          :key="item.to"
          :to="item.to"
          class="flex items-center gap-3 px-3 py-2 rounded-md text-sm font-medium transition-colors"
          :class="route.path.startsWith(item.to)
            ? 'bg-primary-50 text-primary-600'
            : 'text-gray-600 hover:bg-gray-100'"
        >
          <UIcon :name="item.icon" class="w-5 h-5" />
          {{ item.label }}
        </NuxtLink>
      </nav>

      <div class="px-3 py-4 border-t">
        <div class="px-3 py-2 mb-2">
          <p class="text-sm font-medium">{{ authStore.user?.fullName }}</p>
          <p class="text-xs text-gray-400">{{ authStore.user?.role }}</p>
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
    <main class="flex-1 p-6 overflow-y-auto">
      <slot />
    </main>
  </div>
</template>