<script setup lang="ts">
import { klinikGigiV2WebPatientsListList } from '~/client/sdk.gen';
import type { PatientRecord } from '~/client/types.gen';

definePageMeta({
  layout: 'default',
});

const patients = ref<PatientRecord[]>([]);
const loading = ref(true);
const search = ref('');

const page = ref(1);
const pageSize = ref(10);
const totalItems = ref(0);

const isCreateOpen = ref(false);
const isEditOpen = ref(false);
const editingPatientId = ref<string | null>(null);

async function fetchPatients() {
  loading.value = true;
  const { data } = await klinikGigiV2WebPatientsListList({
    query: {
      search: search.value || null,
      page: page.value,
      pagesize: pageSize.value,
    },
  });
  patients.value = data?.items ?? [];
  totalItems.value = data?.totalItems ?? 0;
  loading.value = false;
}

function openEdit(patientId: string) {
  editingPatientId.value = patientId;
  isEditOpen.value = true;
}

const columns = [
  { accessorKey: 'medicalRecordNo', header: 'No. RM' },
  { accessorKey: 'fullName', header: 'Nama Pasien' },
  { accessorKey: 'phone', header: 'Telp' },
  { accessorKey: 'address', header: 'Alamat' },
  { id: 'actions', header: '' },
];

onMounted(fetchPatients);

let searchTimeout: ReturnType<typeof setTimeout>;
watch(search, () => {
  clearTimeout(searchTimeout);
  searchTimeout = setTimeout(() => {
    page.value = 1;
    fetchPatients();
  }, 400);
});

watch(page, fetchPatients);
</script>

<template>
  <div>
    <div class="flex items-center justify-between mb-6">
      <h1 class="text-2xl font-semibold">Data Pasien</h1>
      <UButton icon="i-lucide-plus" @click="isCreateOpen = true">
        Tambah Pasien
      </UButton>
    </div>

    <UInput
      v-model="search"
      icon="i-lucide-search"
      placeholder="Cari nama pasien..."
      class="mb-4 w-full max-w-sm"
    />

    <UCard>
      <UTable :data="patients" :columns="columns" :loading="loading">
        <template #fullName-cell="{ row }">
          <NuxtLink
            :to="`/patients/${row.original.id}`"
            class="text-primary-500 hover:underline font-medium"
          >
            {{ row.original.fullName }}
          </NuxtLink>
        </template>

        <template #actions-cell="{ row }">
          <div class="flex justify-end">
            <UButton
              icon="i-lucide-pencil"
              size="sm"
              variant="ghost"
              color="neutral"
              @click="openEdit(row.original.id!)"
            />
          </div>
        </template>
      </UTable>

      <template #footer>
        <div class="flex justify-between items-center">
          <p class="text-sm text-gray-500">
            Menampilkan {{ patients.length }} dari {{ totalItems }} pasien
          </p>
          <UPagination
            v-model:page="page"
            :total="totalItems"
            :items-per-page="pageSize"
          />
        </div>
      </template>
    </UCard>

    <PatientsCreatePatientSlideOver
      v-model:open="isCreateOpen"
      @created="fetchPatients"
    />

    <PatientsEditPatientSlideOver
      v-model:open="isEditOpen"
      :patient-id="editingPatientId"
      @updated="fetchPatients"
    />
  </div>
</template>