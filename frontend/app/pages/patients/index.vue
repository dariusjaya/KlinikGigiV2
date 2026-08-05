<script setup lang="ts">
import { klinikGigiV2WebPatientsListList, klinikGigiV2WebPatientsCreateCreate } from '~/client/sdk.gen';
import type { PatientRecord } from '~/client/types.gen';

definePageMeta({
  layout: 'default',
});
const patients = ref<PatientRecord[]>([]);
const loading = ref(true);
const search = ref('');
const isModalOpen = ref(false);
const creating = ref(false);
const errorMessage = ref('');

const page = ref(1);
const pageSize = ref(10);
const totalItems = ref(0);

const form = reactive({
  medicalRecordNo: '',
  fullName: '',
  birthDate: '',
  occupation: '',
  address: '',
  phone: '',
});

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

async function handleCreate() {
  errorMessage.value = '';
  creating.value = true;
  const { data, error } = await klinikGigiV2WebPatientsCreateCreate({ body: form });
  creating.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal menambahkan pasien.';
    return;
  }

  isModalOpen.value = false;
  Object.assign(form, {
    medicalRecordNo: '', fullName: '', birthDate: '',
    occupation: '', address: '', phone: '',
  });
  await fetchPatients();
}

const columns = [
  { accessorKey: 'medicalRecordNo', header: 'No. RM' },
  { accessorKey: 'fullName', header: 'Nama Pasien' },
  { accessorKey: 'phone', header: 'Telp' },
  { accessorKey: 'address', header: 'Alamat' },
];

onMounted(fetchPatients);

let searchTimeout: ReturnType<typeof setTimeout>;
watch(search, () => {
  clearTimeout(searchTimeout);
  searchTimeout = setTimeout(fetchPatients, 400);
});

function openCreateModal() {
  errorMessage.value = '';
  Object.assign(form, {
    medicalRecordNo: '', fullName: '', birthDate: '',
    occupation: '', address: '', phone: '',
  });
  isModalOpen.value = true;
}

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
    <UButton icon="i-lucide-plus" @click="openCreateModal">
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

    <UModal v-model:open="isModalOpen" title="Tambah Pasien Baru">
      <template #body>
        <form class="space-y-4" @submit.prevent="handleCreate">
          <UFormField label="No. Rekam Medis">
            <UInput v-model="form.medicalRecordNo" placeholder="RM-2026-0001" class="w-full" />
          </UFormField>
          <UFormField label="Nama Lengkap">
            <UInput v-model="form.fullName" class="w-full" />
          </UFormField>
          <UFormField label="Tanggal Lahir">
            <UInput v-model="form.birthDate" type="date" class="w-full" />
          </UFormField>
          <UFormField label="Pekerjaan">
            <UInput v-model="form.occupation" class="w-full" />
          </UFormField>
          <UFormField label="Alamat">
            <UTextarea v-model="form.address" class="w-full" />
          </UFormField>
          <UFormField label="Telepon">
            <UInput v-model="form.phone" class="w-full" />
          </UFormField>

          <UAlert
            v-if="errorMessage"
            color="error"
            variant="soft"
            :title="errorMessage"
        />

          <div class="flex justify-end gap-2 pt-2">
          <UButton color="neutral" variant="ghost" @click="isModalOpen = false; errorMessage = ''">
            Batal
            </UButton>
            <UButton type="submit" :loading="creating">
              Simpan
            </UButton>
          </div>
        </form>
      </template>
    </UModal>
  </div>
</template>