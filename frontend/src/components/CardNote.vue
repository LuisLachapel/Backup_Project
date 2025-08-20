<script setup>
import DeleteNoteButton from './DeleteNoteButton.vue'
import EditNoteButton from './EditNoteButton.vue'


const props = defineProps({
  notes: {
    type: Array,
    required: true
  }
})
</script>

<template>
  <div
    v-for="note in props.notes"
    :key="note.id"
    class="max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow-sm "
  >
    <header class="flex items-center justify-between">
      <!-- Título -->
      <div class="flex items-center gap-2">
        <h5 class="text-2xl font-bold">{{ note.title }}</h5>
        <!-- Botón Editar -->
        <EditNoteButton :note="note" />
      </div>

      <!-- Eliminar -->
      <DeleteNoteButton :id="note.id" />
    </header>

    <p class="mb-2">{{ note.description }}</p>
    <p class="text-sm text-gray-500">
      {{ new Date(note.date).toLocaleDateString('es-ES') }}
    </p>
  </div>
</template>

