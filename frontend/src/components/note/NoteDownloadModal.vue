<script setup>
import { ref, watch, computed } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from '../ErrorModal.vue';
import FilterIcon from '@/assets/FilterIcon.vue';
import DeleteFilterIcon from '@/assets/DeleteFilterIcon.vue';
import PdfIcon from '@/assets/PdfIcon.vue';
import ExcelIcon from '@/assets/ExcelIcon.vue';
import DownloadIcon from '@/assets/DownloadIcon.vue';
import DeleteIcon from '@/assets/DeleteIcon.vue';


const props = defineProps({
    show: { type: Boolean, default: false }
});

const emit = defineEmits(["close"]);


const store = useNoteStore()
const startDate = ref("")
const endDate = ref("")
const summary = ref([])
const errorMessage = ref("");
const showError = ref(false);


// Computed que agrupa los datos
const getSummary = computed(() => {
  if (!Array.isArray(summary.value)) return [];
  
  return Object.values(
    summary.value.reduce((acc, item) => {
      const key = `${item.username}-${item.position}`;
      if (!acc[key]) {
        acc[key] = { username: item.username, position: item.position, count: 0 };
      }
      acc[key].count++;
      return acc;
    }, {})
  );
});


// Función que trae datos desde la store
const fetchSummary = async () => {
    try {
        summary.value = await store.getSummary(startDate.value, endDate.value)
    } catch (err) {
        errorMessage.value = err.message || "Error al obtener los datos"
        showError.value = true
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



watch(() => props.show, async (val) => {
    if (val) {
        await fetchSummary()
    }
})

const resetFilter = async () => {
    startDate.value = "";
    endDate.value = "";
    await fetchSummary()
}

</script>
<template>
  <!-- Main modal -->
  <div
    v-if="show"
    class="fixed inset-0 flex items-center justify-center overflow-y-auto overflow-x-hidden bg-black/50 z-50"
  >
    <div
      class="relative w-[95%] sm:w-[90%] md:w-[80%] lg:max-w-lg max-h-[90vh] my-4 overflow-y-auto rounded-lg bg-white shadow-sm dark:bg-gray-700"
    >
      <div class="relative p-4 sm:p-6">
        <!-- Botón de cerrar -->
        <button
          @click="emit('close')"
          type="button"
          class="absolute top-3 right-3 z-10 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
        >
          
          <DeleteIcon  class="w-3 h-3"/>
          <span class="sr-only">Cerrar modal</span>
        </button>

        <!-- Header con filtros -->
        <div class="flex flex-col gap-3 border-b border-gray-200 pb-4 pt-6">
          <h2 class="text-lg font-semibold text-gray-700 dark:text-gray-200 mb-2 text-center sm:text-left">
            Filtros
          </h2>

          <!-- Inputs de fechas -->
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

          <!-- Botones de filtro -->
          <div class="flex justify-center sm:justify-start gap-3 mt-2">
            <button
              @click="fetchSummary"
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
          <div class="overflow-x-auto border rounded-md max-h-80 sm:max-h-96">
            <table class="min-w-full text-sm text-left text-gray-600 dark:text-gray-300">
              <thead class="bg-gray-100 dark:bg-gray-600 text-gray-700 dark:text-gray-200 sticky top-0">
                <tr>
                  <th class="px-4 py-2 border">Nombre</th>
                  <th class="px-4 py-2 border">Cargo</th>
                  <th class="px-4 py-2 border text-center">Registros</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="user in getSummary"
                  :key="user.username"
                  class="hover:bg-gray-50 dark:hover:bg-gray-800"
                >
                  <td class="px-4 py-2 border">{{ user.username }}</td>
                  <td class="px-4 py-2 border">{{ user.position }}</td>
                  <td class="px-4 py-2 border text-center">{{ user.count }}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

        <!-- Footer de descargas -->
        <div class="p-4 text-center border-t border-gray-200">
          <DownloadIcon class="mx-auto mb-3 text-gray-400 w-10 h-10 dark:text-gray-200" />
          <h3 class="mb-4 text-base sm:text-lg font-normal text-gray-500 dark:text-gray-400">
            ¿En qué formato deseas hacer la descarga?
          </h3>

          <div class="flex flex-col sm:flex-row justify-center gap-3">
            <!-- Botón PDF -->
            <button
              @click="handleDownload('pdf')"
              type="button"
              class="flex items-center justify-center gap-2 text-sm font-medium text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg px-4 py-2.5 dark:bg-gray-800 dark:text-gray-200 dark:hover:bg-gray-700 transition"
            >
              <PdfIcon class="h-5" />
              <span>PDF</span>
            </button>

            <!-- Botón Excel -->
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

  <!-- Error modal -->
  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>
