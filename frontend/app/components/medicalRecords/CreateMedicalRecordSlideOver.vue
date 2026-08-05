<script setup lang="ts">
import { klinikGigiV2WebMedicalRecordsCreateCreate } from '~/client/sdk.gen';

const props = defineProps<{
  patientId: string;
}>();

const isOpen = defineModel<boolean>('open', { default: false });

const emit = defineEmits<{
  created: [];
}>();

const creating = ref(false);
const errorMessage = ref('');

const form = reactive({
  visitDate: '',
  diagnosis: '',
  therapy: '',
  notes: '' as string | undefined,
});

function resetForm() {
  errorMessage.value = '';
  Object.assign(form, { visitDate: '', diagnosis: '', therapy: '', notes: undefined });
}

async function handleCreate() {
  errorMessage.value = '';
  creating.value = true;

  const { error } = await klinikGigiV2WebMedicalRecordsCreateCreate({
    path: { patientId: props.patientId },
    body: form,
  });

  creating.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal menambahkan kunjungan.';
    return;
  }

  isOpen.value = false;
  emit('created');
}

watch(isOpen, (open) => {
  if (open) resetForm();
});
</script>

<template>
  <USlideover v-model:open="isOpen" title="Tambah Kunjungan Baru">
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

        <UAlert v-if="errorMessage" color="error" variant="soft" :title="errorMessage" />

        <div class="flex justify-end gap-2 pt-2">
          <UButton color="neutral" variant="ghost" @click="isOpen = false">
            Batal
          </UButton>
          <UButton type="submit" :loading="creating">
            Simpan
          </UButton>
        </div>
      </form>
    </template>
  </USlideover>
</template>