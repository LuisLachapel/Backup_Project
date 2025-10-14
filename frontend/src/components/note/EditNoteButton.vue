<script setup>
import { ref } from 'vue'
import ModalAddNote from '@/components/ModalAddNote.vue'
import { useNoteStore } from '@/stores/noteStore'
import ErrorModal from '@/components/ErrorModal.vue'
import EditIcon from '@/assets/EditIcon.vue'

const props = defineProps({
  note: { type: Object, required: true }
})

const store = useNoteStore()
const showEditModal = ref(false)
const showError = ref(false)
const errorMessage = ref("")

const UpdateNote = async (note) => {
  try {
    await store.updateNote({ ...note, id: props.note.id })
    await store.getAllNotes()
    showEditModal.value = false
    
  } catch (error) {
    errorMessage.value = error.message
        showError.value = true
  }
}
</script>

<template>
  <div class="inline-block">
    <!-- Botón Editar -->

    <EditIcon class="w-6 h-6" @click="showEditModal = true"/>

    <!-- Modal para editar -->
    <ModalAddNote
      :note="props.note"
      :show="showEditModal"
      @close="showEditModal = false"
      @saved="UpdateNote"
    />


    <ErrorModal
  :show="showError" 
      :message="errorMessage" 
      @close="showError = false" 
  />
  </div>
</template>
