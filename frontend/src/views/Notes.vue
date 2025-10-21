<script setup>
import { ref, onMounted } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from '@/components/ErrorModal.vue';
import CardNote from '@/components/note/CardNote.vue';
import FABNote from '@/components/note/FABNote.vue';
import FilterIcon from '@/assets/FilterIcon.vue';
import DeleteFilterIcon from '@/assets/DeleteFilterIcon.vue';
import FilterDate from '@/components/FilterDate.vue';

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
      
      <FilterDate
      v-model:startDate="startDate"
      v-model:endDate="endDate"
      @filter="filterNote"
      @reset="resetFilter"
      />

      <!-- Modal de error -->
      <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />

    </header>
  </div>

  <main class="container mx-auto p-2.5">
      <section class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
          <CardNote :notes="store.notes" :isGlobalView="true"  />
      </section>
  </main>

  <FABNote :isGlobalView="true"/>
</template>
