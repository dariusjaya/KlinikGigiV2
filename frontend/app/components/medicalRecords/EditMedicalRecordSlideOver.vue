<script setup lang="ts">
import { klinikGigiV2WebMedicalRecordsGetGet, klinikGigiV2WebMedicalRecordsUpdateUpdate } from '~/client/sdk.gen';

const props = defineProps<{
  patientId: string;
  medicalRecordId: string | null;
}>();

const isOpen = defineModel<boolean>('open', { default: false });

const emit = defineEmits<{
  updated: [];
}>();

const loading = ref(false);
const saving = ref(false);
const errorMessage = ref('');

const form = reactive({
  visitDate: '',
  diagnosis: '',
  therapy: '',
  notes: '' as string | undefined,
});

async function fetchRecord() {
  if (!props.medicalRecordId) return;
  loading.value = true;
  errorMessage.value = '';

  const { data } = await klinikGigiV2WebMedicalRecordsGetGet({
    path: { patientId: props.patientId, medicalRecordId: props.medicalRecordId },
  });
  const record = data?.medicalRecord;

  if (record) {
    Object.assign(form, {
      visitDate: record.visitDate ?? '',
      diagnosis: record.diagnosis ?? '',
      therapy: record.therapy ?? '',
      notes: record.notes ?? undefined,
    });
  }
  loading.value = false;
}

async function handleSave() {
  if (!props.medicalRecordId) return;
  errorMessage.value = '';
  saving.value = true;

  const { error } = await klinikGigiV2WebMedicalRecordsUpdateUpdate({
    path: { patientId: props.patientId, medicalRecordId: props.medicalRecordId },
    body: form,
  });

  saving.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal memperbarui rekam medis.';
    return;
  }

  isOpen.value = false;
  emit('updated');
}

watch(
  () => [props.medicalRecordId, isOpen.value] as const,
  ([id, open]) => {
    if (open && id) fetchRecord();
  },
);
</script>

<template>
  <USlideover v-model:open="isOpen" title="Edit Kunjungan">
    <template #body>
      <div v-if="loading" class="flex justify-center py-10">
        <UIcon name="i-lucide-loader-2" class="w-6 h-6 animate-spin text-gray-400" />
      </div>

      <form v-else class="space-y-4" @submit.prevent="handleSave">
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

        <UAlert v-if="errorMessage" color="error" variant="soft" :title="errorMessage" />

        <div class="flex justify-end gap-2 pt-2">
          <UButton color="neutral" variant="ghost" @click="isOpen = false">
            Batal
          </UButton>
          <UButton type="submit" :loading="saving">
            Simpan
          </UButton>
        </div>
      </form>
    </template>
  </USlideover>
</template>