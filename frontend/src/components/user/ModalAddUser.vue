<script setup>
import { useUserStore } from '@/stores/userStore';
import { ref, watch, onMounted } from "vue"

const props = defineProps({
    show: {type: Boolean, default: false},
    user: {type: Object, default: null}
})

const emit = defineEmits(["close", "saved"])

const store = useUserStore()

const name = ref('')
const positionId = ref(null)

onMounted(() =>{
    store.getAllPosition();
})

watch(
  () => props.show,
  async (val) => {
    if (val && props.user && props.user.id) {
      try {
        const data = await store.getById(props.user.id)
        name.value = data.name
        positionId.value = data.positionId ?? null 
      } catch (error) {
        console.error("Error cargando la nota:", error)
      }
    }
    if (val && !props.user) {
      name.value = ""
      positionId.value = null
    }
  }
)

const saveNote = () => {
  if (props.user) {
    // Editar
    const data = {
      id: props.user.id,
      name: name.value,
      positionId: positionId.value 
    }
    emit("saved", data)
  } else {
    // Crear
    const data = {
      name: name.value,
      positionId: positionId.value 
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


</template>