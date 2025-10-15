<script setup>
import { ref, onMounted } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from '@/components/ErrorModal.vue';
import CardNote from '@/components/note/CardNote.vue';
import FABNote from '@/components/note/FABNote.vue';
import FilterIcon from '@/assets/FilterIcon.vue';
import DeleteFilterIcon from '@/assets/DeleteFilterIcon.vue';

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
  <div class="py-4 ">
    <header class="container mx-auto px-2.5 flex items-center justify-between ">

      
      <h2 class="text-2xl font-bold">Registros</h2>
      
      <div class="flex flex-wrap items-center gap-2">
        <h2 class="text-lg font-bold">Filtrar</h2>

        <!-- Inputs de fecha -->
        <input type="date" v-model="startDate" class="border px-2 py-1 rounded" />
        <input type="date" v-model="endDate" class="border px-2 py-1 rounded" />

        <!-- Botón Filtrar -->
        <button 
          @click="filterNote" 
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

      <!-- Modal de error -->
      <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

    </header>
  </div>

  <main class="container mx-auto p-2.5">
      <section class="grid grid-cols-3 gap-4">
          <CardNote :notes="store.notes" :isGlobalView="true"  />
      </section>
  </main>

  <FABNote :isGlobalView="true"/>
</template>
