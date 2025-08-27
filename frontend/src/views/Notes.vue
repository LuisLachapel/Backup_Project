<script setup>
import { ref, onMounted } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from '@/components/ErrorModal.vue';
import CardNote from '@/components/note/CardNote.vue';
import OpenMenuButton from '@/components/note/OpenMenuButton.vue';

// revisar: https://flowbite.com/docs/components/speed-dial/

const store = useNoteStore();

const startDate = ref("");
const endDate = ref("");
const errorMessage = ref("");
const showError = ref(false);


onMounted(async () => {
    try {
        await store.getAllNotes();
    } catch (err) {
        errorMessage.value = err.message;
        showError.value = true;
    }
});

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
  <div class="py-4 shadow-md">
    <header class="container mx-auto px-2.5 flex items-center justify-between ">

      
      <h2 class="text-2xl font-bold">Backup System</h2>
      
      <div class="flex flex-wrap items-center gap-2">
        <h2 class="text-lg font-bold">Filtrar</h2>

        <!-- Inputs de fecha -->
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
      </div>

      <!-- Modal de error -->
      <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

    </header>
  </div>

  <main class="container mx-auto p-2.5">
      <section class="grid grid-cols-3 gap-4">
          <CardNote :notes="store.notes" />
      </section>
  </main>

  <OpenMenuButton/>
</template>

