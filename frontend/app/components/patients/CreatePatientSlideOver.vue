<script setup lang="ts">
import { klinikGigiV2WebPatientsCreateCreate } from '~/client/sdk.gen';

const isOpen = defineModel<boolean>('open', { default: false });

const emit = defineEmits<{
  created: [];
}>();

const creating = ref(false);
const errorMessage = ref('');

const form = reactive({
  medicalRecordNo: '',
  fullName: '',
  birthDate: undefined as string | undefined,
  occupation: undefined as string | undefined,
  address: '',
  phone: '',
});

function resetForm() {
  errorMessage.value = '';
  Object.assign(form, {
    medicalRecordNo: '',
    fullName: '',
    birthDate: undefined,
    occupation: undefined,
    address: '',
    phone: '',
  });
}

async function handleCreate() {
  errorMessage.value = '';
  creating.value = true;

  const { error } = await klinikGigiV2WebPatientsCreateCreate({ body: form });

  creating.value = false;

  if (error) {
    errorMessage.value = (error as any)?.message ?? 'Gagal menambahkan pasien.';
    return;
  }

  isOpen.value = false;
  emit('created');
}

// Reset form setiap kali slideover dibuka
watch(isOpen, (open) => {
  if (open) resetForm();
});
</script>

<template>
  <USlideover v-model:open="isOpen" title="Tambah Pasien Baru">
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