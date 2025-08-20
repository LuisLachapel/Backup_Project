<script setup>
import { ref } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from './ErrorModal.vue';
import CreateNoteButton from './CreateNoteButton.vue';

const store = useNoteStore();

const startDate = ref("");
const endDate = ref("");
const errorMessage = ref("");
const showError = ref(false);

const filterNote = async () => {
  try {
    await store.filterNoteByDate(startDate.value, endDate.value);
  } catch (err) {
    errorMessage.value = err.message;
    showError.value = true;
  }
};

const resetFilter = async () => {
  try {
    startDate.value = "";
    endDate.value = "";
    await store.getAllNotes(); 
  } catch (err) {
    errorMessage.value = err.message;
    showError.value = true;
  }
};
</script>

<template>
  <div class="p-4 bg-gray-100 rounded-lg shadow-md">
    <h2 class="text-lg font-bold mb-2">Filtrar notas por fecha</h2>

    <div class="flex space-x-2 mb-4">
      <CreateNoteButton/>
      <input type="date" v-model="startDate" class="border px-2 py-1 rounded" />
      <input type="date" v-model="endDate" class="border px-2 py-1 rounded" />

      <!-- Botón Filtrar -->
      <button
        @click="filterNote"
        class="bg-blue-600 text-white px-4 py-1 rounded hover:bg-blue-700"
      >
        Filtrar
      </button>

      <!-- Botón Reset -->
      <button
        v-if="startDate || endDate"
        @click="resetFilter"
        class="bg-gray-500 text-white px-4 py-1 rounded hover:bg-gray-600"
      >
        Resetear
      </button>

      <ErrorModal 
        :show="showError" 
        :message="errorMessage" 
        @close="showError = false" 
      />
    </div>
  </div>


</template>
