<script setup lang="ts">
import {
  klinikGigiV2WebPatientsGetGet,
  klinikGigiV2WebMedicalRecordsListList,
  klinikGigiV2WebMedicalRecordsDeleteDelete,
} from '~/client/sdk.gen';
import type { PatientRecord, MedicalRecordRecord } from '~/client/types.gen';

const route = useRoute();
const patientId = route.params.id as string;

const patient = ref<PatientRecord | null>(null);
const records = ref<MedicalRecordRecord[]>([]);
const loading = ref(true);

const page = ref(1);
const pageSize = ref(10);
const totalItems = ref(0);

const isCreateOpen = ref(false);
const isEditOpen = ref(false);
const editingRecordId = ref<string | null>(null);

const isDeleteOpen = ref(false);
const deletingRecordId = ref<string | null>(null);
const deleting = ref(false);

async function fetchPatient() {
  const { data } = await klinikGigiV2WebPatientsGetGet({ path: { patientId } });
  patient.value = data?.patient ?? null;
}

async function fetchRecords() {
  loading.value = true;
  const { data } = await klinikGigiV2WebMedicalRecordsListList({
    path: { patientId },
    query: { page: page.value, pagesize: pageSize.value },
  });
  records.value = data?.items ?? [];
  totalItems.value = data?.totalItems ?? 0;
  loading.value = false;
}

function openEdit(recordId: string) {
  editingRecordId.value = recordId;
  isEditOpen.value = true;
}

function openDelete(recordId: string) {
  deletingRecordId.value = recordId;
  isDeleteOpen.value = true;
}

async function handleDelete() {
  if (!deletingRecordId.value) return;
  deleting.value = true;

  await klinikGigiV2WebMedicalRecordsDeleteDelete({
    path: { patientId, medicalRecordId: deletingRecordId.value },
  });

  deleting.value = false;
  isDeleteOpen.value = false;
  deletingRecordId.value = null;
  await fetchRecords();
}

const columns = [
  { accessorKey: 'visitDate', header: 'Tanggal' },
  { accessorKey: 'diagnosis', header: 'Diagnosa' },
  { accessorKey: 'therapy', header: 'Therapy' },
  { accessorKey: 'notes', header: 'Catatan' },
  { id: 'actions', header: '' },
];

onMounted(async () => {
  await fetchPatient();
  await fetchRecords();
});

watch(page, fetchRecords);
</script>

<template>
  <div v-if="patient">
    <UButton icon="i-lucide-arrow-left" variant="ghost" :to="`/patients/${patientId}`" class="mb-4">
      Kembali ke Data Pasien
    </UButton>

    <div class="flex items-center justify-between mb-6">
      <div>
        <h1 class="text-2xl font-semibold">Riwayat Kunjungan</h1>
        <p class="text-sm text-gray-500">{{ patient.fullName }} · {{ patient.medicalRecordNo }}</p>
      </div>
      <UButton icon="i-lucide-plus" @click="isCreateOpen = true">
        Tambah Kunjungan
      </UButton>
    </div>

    <UCard>
      <UTable :data="records" :columns="columns" :loading="loading">
        <template #actions-cell="{ row }">
          <div class="flex gap-2 justify-end">
            <UButton
              icon="i-lucide-pencil"
              size="sm"
              variant="ghost"
              color="neutral"
              @click="openEdit(row.original.id!)"
            />
            <UButton
              icon="i-lucide-trash-2"
              size="sm"
              variant="ghost"
              color="error"
              @click="openDelete(row.original.id!)"
            />
          </div>
        </template>
      </UTable>

      <template #footer>
        <div class="flex justify-between items-center">
          <p class="text-sm text-gray-500">
            Menampilkan {{ records.length }} dari {{ totalItems }} kunjungan
          </p>
          <UPagination v-model:page="page" :total="totalItems" :items-per-page="pageSize" />
        </div>
      </template>
    </UCard>

    <MedicalRecordsCreateMedicalRecordSlideOver
      v-model:open="isCreateOpen"
      :patient-id="patientId"
      @created="fetchRecords"
    />

    <MedicalRecordsEditMedicalRecordSlideOver
      v-model:open="isEditOpen"
      :patient-id="patientId"
      :medical-record-id="editingRecordId"
      @updated="fetchRecords"
    />

    <UModal v-model:open="isDeleteOpen" title="Hapus Rekam Medis">
      <template #body>
        <p class="text-sm text-gray-600 mb-4">
          Apakah kamu yakin ingin menghapus rekam medis ini? Tindakan ini tidak bisa dibatalkan.
        </p>
        <div class="flex justify-end gap-2">
          <UButton color="neutral" variant="ghost" @click="isDeleteOpen = false">
            Batal
          </UButton>
          <UButton color="error" :loading="deleting" @click="handleDelete">
            Hapus
          </UButton>
        </div>
      </template>
    </UModal>
  </div>
</template>