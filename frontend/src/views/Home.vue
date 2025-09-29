<script setup>
import { ref, onMounted } from 'vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from '@/components/ErrorModal.vue';
import CardNote from '@/components/note/CardNote.vue';
import FABNote from '@/components/note/FABNote.vue';

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
           <svg class="h-6 w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path opacity="0.4" d="M14.3206 19.0698C14.3206 19.6798 13.9205 20.4798 13.4105 20.7898L12.0005 21.6998C10.6905 22.5098 8.87054 21.5998 8.87054 19.9798V14.6298C8.87054 13.9198 8.47055 13.0098 8.06055 12.5098L4.22052 8.46976C3.71052 7.95976 3.31055 7.05977 3.31055 6.44977V4.12976C3.31055 2.91976 4.22057 2.00977 5.33057 2.00977H18.6705C19.7805 2.00977 20.6906 2.91975 20.6906 4.02975V6.24976C20.6906 7.05976 20.1805 8.06976 19.6805 8.56976" stroke="#292D32" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M16.0692 16.5201C17.8365 16.5201 19.2692 15.0874 19.2692 13.3201C19.2692 11.5528 17.8365 10.1201 16.0692 10.1201C14.3018 10.1201 12.8691 11.5528 12.8691 13.3201C12.8691 15.0874 14.3018 16.5201 16.0692 16.5201Z" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M19.8691 17.1201L18.8691 16.1201" stroke="#292D32" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>

        </button>

        <!-- Botón Reset -->
        <button 
          v-if="startDate || endDate" 
          @click="resetFilter"
           class="bg-red-300 text-white px-4 py-1 rounded-full hover:bg-red-200"
        >
           <svg class="h-6 w-6" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21.6309 14.75C21.6309 15.64 21.3809 16.48 20.9409 17.2C20.1209 18.58 18.6109 19.5 16.8809 19.5C15.1509 19.5 13.6409 18.57 12.8209 17.2C12.3809 16.49 12.1309 15.64 12.1309 14.75C12.1309 12.13 14.2609 10 16.8809 10C19.5009 10 21.6309 12.13 21.6309 14.75Z" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M18.1487 15.99L15.6387 13.48" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path d="M18.1291 13.5098L15.6191 16.0198" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> <path opacity="0.4" d="M20.6905 4.02002V6.23999C20.6905 7.04999 20.1805 8.06001 19.6805 8.57001L17.9205 10.12C17.5905 10.04 17.2405 10 16.8805 10C14.2605 10 12.1305 12.13 12.1305 14.75C12.1305 15.64 12.3805 16.48 12.8205 17.2C13.1905 17.82 13.7005 18.35 14.3205 18.73V19.07C14.3205 19.68 13.9205 20.49 13.4105 20.79L12.0005 21.7C10.6905 22.51 8.87055 21.6 8.87055 19.98V14.63C8.87055 13.92 8.46055 13.01 8.06055 12.51L4.22055 8.46997C3.72055 7.95997 3.31055 7.05001 3.31055 6.45001V4.12C3.31055 2.91 4.22055 2 5.33055 2H18.6705C19.7805 2 20.6905 2.91002 20.6905 4.02002Z" stroke="#ff0000" stroke-width="1.5" stroke-miterlimit="10" stroke-linecap="round" stroke-linejoin="round"></path> </g></svg>

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

  <FABNote/>
</template>

