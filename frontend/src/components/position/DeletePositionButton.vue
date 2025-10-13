<script setup>
import { ref } from 'vue';
import { usePositionStore } from '@/stores/positionStore';
import DeleteIcon from '@/assets/DeleteIcon.vue';

const props = defineProps({
    id: {type: Number, required: 7}
})

const store = usePositionStore()
const closeModal = ref(false)

const deletePosition = async() =>{
    await store.deletePosition(props.id);
    closeModal.value = false
}

</script>


<template>

    <button @click="closeModal = true">
         <svg
              
              class="w-6 h-6 cursor-pointer text-gray-600 hover:text-red-600 inline-block"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
            >
              <path
                stroke="currentColor"
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M5 7h14m-9 3v8m4-8v8M10 3h4a1 1 0 0 1 1 1v3H9V4a1 1 0 0 1 1-1ZM6 7h12v13a1 1 0 0 1-1 1H7a1 1 0 0 1-1-1V7Z"
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
        </div>

        <!-- Body -->
        <div class="p-4 text-center">
          <DeleteIcon class="w-12 h-12"/>
          <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
            ¿Estas seguro de que deseas eliminar esto?
          </h3>
          <button
          @click="deletePosition"
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



</template>