<script setup>
import { ref } from 'vue'
import { useNoteStore } from '@/stores/noteStore';
import DeleteIcon from '@/assets/DeleteIcon.vue';
import { useSessionStore } from '@/stores/sessionStore';
import AlertIcon from '@/assets/AlertIcon.vue';
import ErrorModal from '../ErrorModal.vue';


const props = defineProps({
    id:{
        type: Number,
        require: true
    },
    isGlobalView: { type: Boolean, default: false } 
})

const store = useNoteStore()
const sessionStore = useSessionStore()
const closeModal = ref(false)

const errorMessage = ref("");
const showError = ref(false);


const DeleteNote = async () => {
    try {
      
    await store.deleteNote(props.id,props.isGlobalView)
    closeModal.value = false;
    } catch (error) {
      errorMessage.value = error.message || "Ocurrió un error al descargar.";
    showError.value = true;
    }
}
</script>

<template>
  <!-- Botón para abrir el alert -->
  <button
    type="button"
    @click="closeModal = true"
    class="text-gray-400 hover:bg-red-500 hover:text-white rounded-lg text-sm w-8 h-8 inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
  >
    <svg
      class="w-3 h-3"
      aria-hidden="true"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 14 14"
    >
      <path
        stroke="currentColor"
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="2"
        d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
      />
    </svg>
  </button>

  <!-- Alert modal -->
  <div
    v-if="closeModal"
    class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
  >
    <div class="relative p-4 w-full max-w-md max-h-full">
      <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
        <!-- Header -->
        <div class="flex items-center justify-between p-4 border-b rounded-t dark:border-gray-600 border-gray-200">
          <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
            Confirmación
          </h3>
          <button
            type="button"
            @click="closeModal = false"
            class="text-gray-400 hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
          >
          <AlertIcon class="w-3 h-3"/>
           
          </button>
        </div>

        <!-- Body -->
        <div class="p-4 text-center">
          <DeleteIcon class="w-12 h-12"/>
          <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
            ¿Estas seguro de que deseas eliminar esto?
          </h3>
          <button
          @click="DeleteNote"
            type="button"
            class="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center"
          >
            Si, Lo estoy
          </button>
          <button
            @click="closeModal = false"
            type="button"
            class="py-2.5 px-5 ms-3 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
          >
            No, cancelar
          </button>
        </div>
      </div>
    </div>
  </div>

  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>
