<script setup>
import { ref, onMounted, watch } from 'vue';
import { usePositionStore } from '@/stores/positionStore';


const props = defineProps({
  show: {type: Boolean, default: false},
  position: {type: Object, default: null}
})

const emit = defineEmits(['close', 'saved'])
const store = usePositionStore()
const name = ref('')
const description = ref("")

onMounted(() =>{
  store.getAllPosition()
})

watch(
  () => props.show,
  async (val) => {
    if (val && props.position && props.position.id) {
      try {
        const data = await store.getPositionById(props.position.id)
        name.value = data.name
        description.value = data.description
      } catch (error) {
        console.error("Error cargando el cargo:", error)
      }
    }
    if (val && !props.position) {
      name.value = ""
      description.value = ""
    }
  }
)

const savePosition= () => {
  if (props.position) {
    // Editar
    const data = {
      id: props.position.id,
      name: name.value,
      description: description.value 
    }
    emit("saved", data)
  } else {
    // Crear
    const data = {
      name: name.value,
      description: description.value 
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
              {{ props.position ? "Editar cargo" : "Crear cargo" }}
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
          <form class="p-4" @submit.prevent="savePosition">
            <div class="grid gap-4 mb-4">
              <div>
                <label class="block mb-2 text-sm font-medium">Nombre del cargo</label>
                <input
                  v-model="name"
                  type="text"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese el título"
                  maxlength="20"
                />
                <p class="text-sm text-gray-500">{{ name.length }}/20</p>
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
              {{ props.position? "Actualizar" : "Agregar nuevo" }}
            </button>
          </form>
        </div>
      </div>
    </div>
  </teleport>

</template>