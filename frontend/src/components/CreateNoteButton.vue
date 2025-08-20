<script setup>
import { ref } from 'vue';
import ModalAddNote from './ModalAddNote.vue';
import { useNoteStore } from '@/stores/noteStore';
import ErrorModal from './ErrorModal.vue';


const store = useNoteStore()
const showCreateModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")

const CreateNote = async (note) =>{
    try {
        await store.createNote(note)
        showCreateModal.value = false
    } catch (error) {
        errorMessage.value = error.message
        showError.value = true
    }
}

</script>

<template>

    <ModalAddNote :note= "null" :show="showCreateModal" @close = "showCreateModal = false" @saved = "CreateNote"/>
    <button
    @click="showCreateModal = true"
    class="flex size-10 items-center justify-center text-white bg-blue-700 hover:bg-blue-800 rounded-full px-5 py-2.5 text-lg font-semibold"
  >
    + 
  </button>

  <ErrorModal
  :show="showError" 
      :message="errorMessage" 
      @close="showError = false" 
  />


</template>