<script setup>
import { ref } from 'vue'
import ModalAddNote from './ModalAddNote.vue'
import { useNoteStore } from '@/stores/noteStore'

const props = defineProps({
  note: { type: Object, default: null }, // Si existe: editar, sino: crear
  label: { type: String, default: '' }  // Texto del botón (opcional)
})

const store = useNoteStore()
const showModal = ref(false)

// Función común para crear o actualizar
const saveNote = async (noteData) => {
  if (props.note) {
    // Modo editar
    await store.updateNote({ ...noteData, id: props.note.id })
  } else {
    // Modo crear
    await store.createNote(noteData)
  }
  await store.getAllNotes()
  showModal.value = false
}
</script>

<template>
  <div class="inline-block">
    <!-- Botón -->
    <button
      @click="showModal = true"
      class="block text-white bg-blue-700 hover:bg-blue-800 rounded-full px-5 py-2.5"
    >
      <slot>
        {{ props.note ? 'Editar Nota' : '+ Nueva Nota' }}
      </slot>
    </button>

    <!-- Modal -->
    <ModalAddNote
      :note="props.note"
      :show="showModal"
      @close="showModal = false"
      @saved="saveNote"
    />
  </div>
</template>
