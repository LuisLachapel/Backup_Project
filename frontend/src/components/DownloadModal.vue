<script setup>
import { ref, watch } from "vue";
import { useUserStore } from "@/stores/userStore";
import ErrorModal from "./ErrorModal.vue";
import PdfIcon from "@/assets/PdfIcon.vue";
import ExcelIcon from "@/assets/ExcelIcon.vue";
import FilterIcon from "@/assets/FilterIcon.vue";
import DeleteFilterIcon from "@/assets/DeleteFilterIcon.vue";
import DownloadIcon from "@/assets/DownloadIcon.vue";

const props = defineProps({
  show: { type: Boolean, default: false }
});

const emit = defineEmits(["close"]);
const store = useUserStore();

const users = ref([]);
const startDate = ref("");
const endDate = ref("");
const errorMessage = ref("");
const showError = ref(false);

async function getSummary(start = null, end = null) {
  try {
    users.value = await store.getSummary(start, end);
  } catch (err) {
    errorMessage.value = err.message;
    showError.value = true;
  }
}

const handleDownload = async (type) => {
  try {
    await store.download(type, startDate.value, endDate.value);
  } catch (err) {
    errorMessage.value = err.message || "Ocurrió un error al descargar.";
    showError.value = true;
  }
};


const resetFilter = async () => {
  startDate.value = "";
  endDate.value = "";
  await getSummary();
};


watch(
  () => props.show,
  async (val) => {
    if (val) {
      await getSummary(); 
    }
  }
);
</script>


<template>
    

<!-- Main modal -->
<div v-if="show" tabindex="-1"  class="fixed inset-0 flex items-center overflow-y-auto overflow-x-hidden justify-center w-full  max-h-full bg-black/50">
    <div class="relative w-full max-w-lg max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
            <!--Boton de cerrar-->
            <button @click="emit('close')" type="button" class="absolute top-3 end-2.5 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="popup-modal">
                <svg class="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                </svg>
                <span class="sr-only">Cerrar modal</span>
            </button>
            <!-- Header con filtros -->
        <div class="flex flex-col gap-2 p-4 border-b border-gray-200 pt-6">
          <h2 class="text-lg font-semibold text-gray-700 dark:text-gray-200 mb-2">Filtros</h2>
          <div class="flex gap-2">
            <input
              type="date"
              v-model="startDate"
              class="border px-2 py-1 rounded w-1/2"
            />
            <input
              type="date"
              v-model="endDate"
              class="border px-2 py-1 rounded w-1/2"
            />
          </div>
          <div class="flex gap-2">
          <!-- Botón Filtrar -->
            <button
              @click="getSummary(startDate, endDate)" 
              class="bg-white-200 text-white px-4 py-1 rounded-full hover:bg-gray-100"
            >
            <FilterIcon class="h-6 w-6"/>
            </button>
            <!-- Botón Reset -->
            <button
              v-if="startDate || endDate"
              @click="resetFilter"
              class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
            >
            <DeleteFilterIcon class="h-6 w-6"/>
            </button>
          </div>
        </div>
        <!-- Tabla con datos -->
      <div class="p-4">
        <div class="overflow-y-auto max-h-96 border rounded">
          <table class="min-w-full text-sm text-left text-gray-500 dark:text-gray-300 border">
            <thead class="bg-gray-100 dark:bg-gray-600 text-gray-700 dark:text-gray-200">
              <tr>
                <th class="px-4 py-2 border">ID</th>
                <th class="px-4 py-2 border">Nombre</th>
                <th class="px-4 py-2 border">Posición</th>
                <th class="px-2 py-2 border">Registros</th>
                <th class="px-4 py-2 border">Estado</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="user in users" :key="user.id"
                class="hover:bg-gray-50 dark:hover:bg-gray-600">
                <td class="px-4 py-2 border">{{ user.id }}</td>
                <td class="px-4 py-2 border">{{ user.name }}</td>
                <td class="px-4 py-2 border">{{ user.position }}</td>
                <td class="px-2 py-2 border">{{ user.records }}</td>
                <td class="px-4 py-2 border">
                  <span :class="user.status === 'activo' ? 'text-green-600 font-semibold' : 'text-red-600 font-semibold'">
                    {{ user.status }}
                  </span>
                </td>
              </tr>
              <tr v-if="users.length === 0">
                <td colspan="5" class="text-center py-3 text-gray-400">No hay datos disponibles</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <!--Footer de descargas-->
            <div class="p-4 md:p-5 text-center">
               <DownloadIcon class="mx-auto mb-4 text-gray-400 w-12 h-12 dark:text-gray-200"/>
               <h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">¿En que formato deseas hacer la descarga?</h3>
                <button @click="handleDownload('pdf')"  data-modal-hide="popup-modal" type="button" class=" mr-4 text-white bg-gray-50 hover:bg-gray-100 focus:ring-4 focus:outline-none dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center">
                    <PdfIcon class="h-5"/>
                  <span class="flex-1 ms-3 text-black whitespace-nowrap">Pdf</span>

                </button>
                <button @click="handleDownload('excel')" data-modal-hide="popup-modal" type="button" class="text-white bg-gray-50 hover:bg-gray-100 focus:ring-4 focus:outline-none dark:focus:ring-red-800 font-medium rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center">
                              <ExcelIcon class="h-5"/>
                            <span class="flex-1 ms-3 text-black whitespace-nowrap">Excel</span>


                </button>
            </div>
        </div>
    </div>
</div>

  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

</template>