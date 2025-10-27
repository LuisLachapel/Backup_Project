<script setup>
import { ref, onMounted } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import { useSessionStore } from '@/stores/sessionStore';
import ErrorModal from '@/components/ErrorModal.vue';
import CardNote from '@/components/note/CardNote.vue';
import FABNote from '@/components/note/FABNote.vue';
import FilterDate from '@/components/FilterDate.vue';

const store = useNoteStore();
const sessionStore = useSessionStore()
const user = sessionStore.currentUser
const startDate = ref("");
const endDate = ref("");
const errorMessage = ref("");
const showError = ref(false);


onMounted(async () => {
    try {
        
        await store.getNotesByUser(user.id)
    } catch (err) {
        errorMessage.value = err.message;
        showError.value = true;
    }
});

const filterNote = async () => {
    try {
        await store.filterNoteByDateForUser(startDate.value, endDate.value);
    } catch (err) {
        errorMessage.value = err.message;
        showError.value = true;
    }
};

const resetFilter = async () => {
    try {
        startDate.value = "";
        endDate.value = "";
        await store.getNotesByUser(sessionStore.currentUser.id);
    } catch (err) {
        errorMessage.value = err.message;
        showError.value = true;
    }
};
</script>

<template>
  <div class="py-4">
    <header class="container mx-auto px-2.5">
      <!-- Título -->
      <h2 class="text-2xl font-bold mb-4">Mis Registros</h2>

      <!-- Filtro debajo del título -->
      <div class="w-full mb-4">
        <FilterDate
          v-model:startDate="startDate"
          v-model:endDate="endDate"
          @filter="filterNote"
          @reset="resetFilter"
        />
      </div>

      <!-- Modal de error -->
      <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
    </header>
  </div>

  <main class="container mx-auto p-2.5">
    <section
      v-if="store.notes.length > 0"
      class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4"
    >
      <CardNote :notes="store.notes" :isGlobalView="false" />
    </section>

    <section v-else class="text-center text-gray-500 py-10">
      <p>No tienes registros disponibles.</p>
    </section>
  </main>

  <FABNote :isGlobalView="false" />
</template>



