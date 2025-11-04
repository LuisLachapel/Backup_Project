<script setup>
import { ref, defineEmits, defineProps } from 'vue';
import DeleteIcon from '@/assets/DeleteIcon.vue';
import UserIcon from '@/assets/UserIcon.vue';

const props = defineProps({
  show: { type: Boolean, default: false },
  user: { type: Object, default: null },
});

const emit = defineEmits(['close', 'confirm']);

const password = ref('');
const error = ref('');

const confirm = () => {
  if (!password.value) {
    error.value = 'Por favor, ingresa la contraseña.';
    return;
  }
  emit('confirm', { user: props.user, password: password.value });
  password.value = '';
  error.value = '';
};

const close = () => {
  emit('close');
  password.value = '';
  error.value = '';
};
</script>

<template>
  <div
    v-if="show"
    class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
  >
    <div class="relative p-4 w-full max-w-md max-h-full">
      <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
        <!-- Header -->
        <div
          class="flex items-center justify-between p-4 border-b rounded-t dark:border-gray-600 border-gray-200"
        >
          <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
            Ingresar contraseña
          </h3>
          <button
            type="button"
            @click="close"
            class="text-gray-400 hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
          >
            <DeleteIcon class="w-3 h-3" />
          </button>
        </div>

        <!-- Body -->
        <div class="p-6 text-center">
          <UserIcon class="w-12 h-12 mx-auto mb-4" />
          <p class="mb-3 text-lg text-gray-700 dark:text-gray-300">
            Ingresa la contraseña para <b>{{ props.user?.name }}</b>
          </p>
          <input
            v-model="password"
            type="password"
            placeholder="Contraseña"
            class="w-full p-2.5 border border-gray-300 rounded-lg focus:ring focus:ring-blue-200"
          />
          <p v-if="error" class="text-red-500 text-sm mt-1">{{ error }}</p>

          <div class="flex justify-center mt-6 space-x-3">
            <button
              @click="confirm"
              class="bg-blue-600 text-white px-5 py-2.5 rounded-lg hover:bg-blue-700"
            >
              Confirmar
            </button>
            <button
              @click="close"
              class="px-5 py-2.5 rounded-lg border border-gray-300 text-gray-700 hover:bg-gray-100"
            >
              Cancelar
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
