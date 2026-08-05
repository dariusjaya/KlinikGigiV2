<script setup lang="ts">
import {
  klinikGigiV2WebPatientsGetGet,
  klinikGigiV2WebMedicalRecordsListList,
  klinikGigiV2WebMedicalRecordsCreateCreate,
} from '~/client/sdk.gen';
import type { PatientRecord, MedicalRecordRecord } from '~/client/types.gen';

const route = useRoute();
const patientId = route.params.id as string;

const patient = ref<PatientRecord | null>(null);
const records = ref<MedicalRecordRecord[]>([]);
const loading = ref(true);
const isModalOpen = ref(false);
const creating = ref(false);
const errorMessage = ref('');

const page = ref(1);
const pageSize = ref(10);
const totalItems = ref(0);

const form = reactive({
  visitDate: '',
  diagnosis: '',
  therapy: '',
  notes: '',
});

async function fetchPatient() {
  const { data } = await klinikGigiV2WebPatientsGetGet({ path: { patientId } });
  patient.value = data?.patient ?? null;
}

async function fetchRecords() {
  loading.value = true;
  const { data } = await klinikGigiV2WebMedicalRecordsListList({
    path: { patientId },
    query: {
      page: page.value,
      pagesize: pageSize.value,
    },
  });
  records.value = data?.items ?? [];
  totalItems.value = data?.totalItems ?? 0;
  loading.value = false;
}

async function handleCreate() {
  errorMessage.value = '';
  creating.value = true;
  const { data, error } = await klinikGigiV2WebMedicalRecordsCreateCreate({
    path: { patientId },
    body: form,
  });
  creating.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal menambahkan kunjungan.';
    return;
  }

  isModalOpen.value = false;
  Object.assign(form, { visitDate: '', diagnosis: '', therapy: '', notes: '' });
  page.value = 1;
  await fetchRecords();
}

function openCreateModal() {
  errorMessage.value = '';
  Object.assign(form, { visitDate: '', diagnosis: '', therapy: '', notes: '' });
  isModalOpen.value = true;
}

const columns = [
  { accessorKey: 'visitDate', header: 'Tanggal' },
  { accessorKey: 'diagnosis', header: 'Diagnosa' },
  { accessorKey: 'therapy', header: 'Therapy' },
  { accessorKey: 'notes', header: 'Catatan' },
];

onMounted(async () => {
  await fetchPatient();
  await fetchRecords();
});

watch(page, fetchRecords);
</script>

<template>
  <div v-if="patient">
    <UButton icon="i-lucide-arrow-left" variant="ghost" to="/patients" class="mb-4">
      Kembali
    </UButton>

    <UCard class="mb-6">
      <div class="grid grid-cols-2 gap-4 text-sm">
        <div><span class="text-gray-500">No. RM:</span> {{ patient.medicalRecordNo }}</div>
        <div><span class="text-gray-500">Nama:</span> {{ patient.fullName }}</div>
        <div><span class="text-gray-500">Tgl Lahir:</span> {{ patient.birthDate }}</div>
        <div><span class="text-gray-500">Pekerjaan:</span> {{ patient.occupation }}</div>
        <div><span class="text-gray-500">Alamat:</span> {{ patient.address }}</div>
        <div><span class="text-gray-500">Telp:</span> {{ patient.phone }}</div>
      </div>
    </UCard>

    <div class="flex items-center justify-between mb-4">
      <h2 class="text-lg font-semibold">Riwayat Kunjungan</h2>
      <UButton icon="i-lucide-plus" @click="openCreateModal">
        Tambah Kunjungan
      </UButton>
    </div>

    <UCard>
      <UTable :data="records" :columns="columns" :loading="loading" />

      <template #footer>
        <div class="flex justify-between items-center">
          <p class="text-sm text-gray-500">
            Menampilkan {{ records.length }} dari {{ totalItems }} kunjungan
          </p>
          <UPagination
            v-model:page="page"
            :total="totalItems"
            :items-per-page="pageSize"
          />
        </div>
      </template>
    </UCard>

    <UModal v-model:open="isModalOpen" title="Tambah Kunjungan Baru">
      <template #body>
        <form class="space-y-4" @submit.prevent="handleCreate">
          <UFormField label="Tanggal Kunjungan">
            <UInput v-model="form.visitDate" type="date" class="w-full" />
          </UFormField>
          <UFormField label="Diagnosa">
            <UTextarea v-model="form.diagnosis" class="w-full" />
          </UFormField>
          <UFormField label="Therapy">
            <UTextarea v-model="form.therapy" class="w-full" />
          </UFormField>
          <UFormField label="Catatan (opsional)">
            <UTextarea v-model="form.notes" class="w-full" />
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