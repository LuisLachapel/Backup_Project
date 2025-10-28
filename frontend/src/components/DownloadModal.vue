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
  <div
    v-if="show"
    tabindex="-1"
    class="fixed inset-0 flex items-center justify-center overflow-y-auto overflow-x-hidden bg-black/50 z-50"
  >
    <!-- Contenedor adaptable -->
    <div
      class="relative w-[95%] sm:w-[90%] md:w-[80%] lg:max-w-3xl max-h-[90vh] my-4 overflow-y-auto rounded-lg bg-white shadow-sm dark:bg-gray-700"
    >
      <div class="relative p-4 sm:p-6">
        <!-- Botón de cerrar -->
        <button
          @click="emit('close')"
          type="button"
          class="absolute top-3 right-3 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
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
          <span class="sr-only">Cerrar modal</span>
        </button>

        <!-- Header con filtros -->
        <div class="flex flex-col gap-3 border-b border-gray-200 pb-4 pt-6">
          <h2
            class="text-lg font-semibold text-gray-700 dark:text-gray-200 text-center sm:text-left"
          >
            Filtros
          </h2>

          <!-- Inputs de fecha -->
          <div class="flex flex-col sm:flex-row gap-2">
            <input
              type="date"
              v-model="startDate"
              class="border px-3 py-2 rounded w-full text-sm dark:bg-gray-800 dark:border-gray-600 dark:text-gray-200"
            />
            <input
              type="date"
              v-model="endDate"
              class="border px-3 py-2 rounded w-full text-sm dark:bg-gray-800 dark:border-gray-600 dark:text-gray-200"
            />
          </div>

          <!-- Botones de filtros -->
          <div class="flex justify-center sm:justify-start gap-3 mt-2">
            <button
              @click="getSummary(startDate, endDate)"
              class="bg-white-200 text-white px-4 py-1 rounded-full hover:bg-gray-100"
            >
              <FilterIcon class="h-5 w-5" />
            </button>

            <button
              v-if="startDate || endDate"
              @click="resetFilter"
              class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
            >
              <DeleteFilterIcon class="h-5 w-5" />
            </button>
          </div>
        </div>

        <!-- Tabla de datos -->
        <div class="p-3">
          <div
            class="overflow-x-auto overflow-y-auto border rounded-md max-h-[70vh]"
          >
            <table
              class="min-w-full text-sm text-left text-gray-600 dark:text-gray-300"
            >
              <thead
                class="bg-gray-100 dark:bg-gray-600 text-gray-700 dark:text-gray-200 sticky top-0"
              >
                <tr>
                  <th class="px-4 py-2 border">ID</th>
                  <th class="px-4 py-2 border">Nombre</th>
                  <th class="px-4 py-2 border">Posición</th>
                  <th class="px-2 py-2 border">Registros</th>
                  <th class="px-4 py-2 border">Estado</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="user in users"
                  :key="user.id"
                  class="hover:bg-gray-50 dark:hover:bg-gray-800"
                >
                  <td class="px-4 py-2 border">{{ user.id }}</td>
                  <td class="px-4 py-2 border">{{ user.name }}</td>
                  <td class="px-4 py-2 border">{{ user.position }}</td>
                  <td class="px-2 py-2 border text-center">{{ user.records }}</td>
                  <td class="px-4 py-2 border text-center">
                    <span
                      :class="user.status === 'activo'
                        ? 'text-green-600 font-semibold'
                        : 'text-red-600 font-semibold'"
                    >
                      {{ user.status }}
                    </span>
                  </td>
                </tr>

                <!-- Fila vacía -->
                <tr v-if="users.length === 0">
                  <td
                    colspan="5"
                    class="text-center py-3 text-gray-400 dark:text-gray-500"
                  >
                    No hay datos disponibles
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

        <!-- Footer -->
        <div class="p-4 text-center border-t border-gray-200">
          <DownloadIcon
            class="mx-auto mb-3 text-gray-400 w-10 h-10 dark:text-gray-200"
          />
          <h3
            class="mb-4 text-base sm:text-lg font-normal text-gray-500 dark:text-gray-400"
          >
            ¿En qué formato deseas hacer la descarga?
          </h3>

          <div class="flex flex-col sm:flex-row justify-center gap-3">
            <!-- PDF -->
            <button
              @click="handleDownload('pdf')"
              type="button"
              class="flex items-center justify-center gap-2 text-sm font-medium text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg px-4 py-2.5 dark:bg-gray-800 dark:text-gray-200 dark:hover:bg-gray-700 transition"
            >
              <PdfIcon class="h-5" />
              <span>PDF</span>
            </button>

            <!-- Excel -->
            <button
              @click="handleDownload('excel')"
              type="button"
              class="flex items-center justify-center gap-2 text-sm font-medium text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg px-4 py-2.5 dark:bg-gray-800 dark:text-gray-200 dark:hover:bg-gray-700 transition"
            >
              <ExcelIcon class="h-5" />
              <span>Excel</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Modal de error -->
  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>
