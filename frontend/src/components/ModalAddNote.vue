<script setup>
import { ref, watch, onMounted } from "vue"
import { useNoteStore } from "@/stores/noteStore"
import { useSessionStore } from "@/stores/sessionStore"

const props = defineProps({
  show: { type: Boolean, default: false },
  note: { type: Object, default: null }
})

const emit = defineEmits(["close", "saved"])

const noteStore = useNoteStore();
const session = useSessionStore();

const title = ref("")
const description = ref("")
const userId = ref(null)

onMounted(() =>{
  noteStore.getAllUser();
})

watch(
  () => props.show,
  async (val) => {
    if (val && props.note && props.note.id) {
      try {
        const data = await noteStore.getById(props.note.id)
        title.value = data.title
        description.value = data.description
        userId.value = data.userId ?? null 
      } catch (error) {
        console.error("Error cargando la nota:", error)
      }
    }
    if (val && !props.note) {
      title.value = ""
      description.value = ""
      userId.value = null
    }
  }
)

const saveNote = () => {
  if (props.note) {
    // Editar
    const data = {
      id: props.note.id,
      title: title.value,
      description: description.value
      
    }
    emit("saved", data)
  } else {
    // Crear
    const data = {
      title: title.value,
      description: description.value,
      userId: session.currentUser.id 
    }
    emit("saved", data)
  }
  close()
}

const close = () => {
  emit("close")
}
</script>


<template>
  <teleport to="body">
    <div
      v-if="show"
      class="fixed inset-0 z-50 flex items-center justify-center bg-black/50"
    >
      <div class="relative p-4 w-full max-w-md max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
          <!-- Header -->
          <div class="flex items-center justify-between p-4 border-b border-gray-200">
            <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
              {{ props.note ? "Editar nota" : "Crear nota" }}
            </h3>
            <button
              type="button"
              @click="emit('close')"
              class="text-gray-400 hover:bg-gray-200 rounded-lg w-8 h-8 flex justify-center items-center"
            >
              ✕
            </button>
          </div>

          <!-- Body -->
          <form class="p-4" @submit.prevent="saveNote">
            <div class="grid gap-4 mb-4">
              <div>
                <label class="block mb-2 text-sm font-medium">Título</label>
                <input
                  v-model="title"
                  type="text"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese el título"
                  maxlength="50"
                />
                <p class="text-sm text-gray-500">{{ title.length }}/20</p>
              </div>

              <div>
                <label class="block mb-2 text-sm font-medium">Descripción</label>
                <textarea
                  v-model="description"
                  rows="4"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese la descripción"
                  maxlength="200"
                ></textarea>
                <p class="text-sm text-gray-500">{{ description.length }}/200</p>
              </div>
            </div>

            <button
              type="submit"
              class="w-full text-white bg-blue-700 hover:bg-blue-800 rounded-lg px-5 py-2.5"
            >
              {{ props.note ? "Actualizar" : "Agregar nuevo" }}
            </button>
          </form>
        </div>
      </div>
    </div>
  </teleport>
</template>
