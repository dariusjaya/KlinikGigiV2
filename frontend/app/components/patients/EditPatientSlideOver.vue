<script setup lang="ts">
import { klinikGigiV2WebPatientsGetGet, klinikGigiV2WebPatientsUpdateUpdate } from '~/client/sdk.gen';

const props = defineProps<{
  patientId: string | null;
}>();

const isOpen = defineModel<boolean>('open', { default: false });

const emit = defineEmits<{
  updated: [];
}>();

const loading = ref(false);
const saving = ref(false);
const errorMessage = ref('');

const form = reactive({
  fullName: '',
  birthDate: undefined as string | undefined,
  occupation: undefined as string | undefined,
  address: '',
  phone: '',
});

async function fetchPatient() {
  if (!props.patientId) return;
  loading.value = true;
  errorMessage.value = '';

  const { data } = await klinikGigiV2WebPatientsGetGet({ path: { patientId: props.patientId } });
  const patient = data?.patient;

  if (patient) {
    Object.assign(form, {
      fullName: patient.fullName ?? '',
      birthDate: patient.birthDate ?? undefined,
      occupation: patient.occupation ?? undefined,
      address: patient.address ?? '',
      phone: patient.phone ?? '',
    });
  }
  loading.value = false;
}

async function handleSave() {
  if (!props.patientId) return;
  errorMessage.value = '';
  saving.value = true;

  const { error } = await klinikGigiV2WebPatientsUpdateUpdate({
    path: { patientId: props.patientId },
    body: form,
  });

  saving.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal memperbarui data pasien.';
    return;
  }

  isOpen.value = false;
  emit('updated');
}

// Setiap kali slideover dibuka dengan patientId baru, fetch ulang datanya
watch(
  () => [props.patientId, isOpen.value] as const,
  ([id, open]) => {
    if (open && id) {
      fetchPatient();
    }
  },
);
</script>

<template>
  <USlideover v-model:open="isOpen" title="Edit Data Pasien">
    <template #body>
      <div v-if="loading" class="flex justify-center py-10">
        <UIcon name="i-lucide-loader-2" class="w-6 h-6 animate-spin text-gray-400" />
      </div>

      <form v-else class="space-y-4" @submit.prevent="handleSave">
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