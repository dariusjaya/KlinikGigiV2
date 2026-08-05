<script setup lang="ts">
import { klinikGigiV2WebPatientsGetGet } from '~/client/sdk.gen';
import type { PatientRecord } from '~/client/types.gen';

const route = useRoute();
const patientId = route.params.id as string;

const patient = ref<PatientRecord | null>(null);
const loading = ref(true);
const isEditOpen = ref(false);

async function fetchPatient() {
  loading.value = true;
  const { data } = await klinikGigiV2WebPatientsGetGet({ path: { patientId } });
  patient.value = data?.patient ?? null;
  loading.value = false;
}

onMounted(fetchPatient);
</script>

<template>
  <div v-if="patient">
    <UButton icon="i-lucide-arrow-left" variant="ghost" to="/patients" class="mb-4">
      Kembali
    </UButton>

    <UCard class="mb-6">
      <template #header>
        <div class="flex items-center justify-between">
          <h2 class="font-semibold">Data Pasien</h2>
          <UButton icon="i-lucide-pencil" size="sm" variant="soft" @click="isEditOpen = true">
            Edit
          </UButton>
        </div>
      </template>

      <div class="grid grid-cols-2 gap-4 text-sm">
        <div><span class="text-gray-500">No. RM:</span> {{ patient.medicalRecordNo }}</div>
        <div><span class="text-gray-500">Nama:</span> {{ patient.fullName }}</div>
        <div><span class="text-gray-500">Tgl Lahir:</span> {{ patient.birthDate ?? '-' }}</div>
        <div><span class="text-gray-500">Pekerjaan:</span> {{ patient.occupation ?? '-' }}</div>
        <div><span class="text-gray-500">Alamat:</span> {{ patient.address }}</div>
        <div><span class="text-gray-500">Telp:</span> {{ patient.phone }}</div>
      </div>
    </UCard>

    <UButton
      icon="i-lucide-clipboard-list"
      :to="`/patients/${patientId}/medicalRecord`"
      block
      size="lg"
    >
      Lihat Riwayat Kunjungan
    </UButton>

    <PatientsEditPatientSlideOver
      v-model:open="isEditOpen"
      :patient-id="patientId"
      @updated="fetchPatient"
    />
  </div>
</template>