<script setup>
import { useUserStore } from '@/stores/userStore';
import { usePermissionStore } from '@/stores/permissionStore';
import { ref, watch, onMounted } from "vue"
import PermissionCheckBox from '../permission/PermissionCheckBox.vue';
import ErrorModal from '../ErrorModal.vue';

const props = defineProps({
    show: {type: Boolean, default: false},
    user: {type: Object, default: null}
})

const emit = defineEmits(["close", "saved"])

const store = useUserStore()
const showError = ref(false)
const errorMessage = ref("")
const permissionStore = usePermissionStore()
const selectedPermissions = ref([]) 

const name = ref('')
const positionId = ref(null)

onMounted(() =>{
    store.getAllPosition();
    permissionStore.getPermissions();
})

watch(
  () => props.show,
  async () => {
    if (props.show && props.user && props.user.id) {
      try {
        const data = await store.getById(props.user.id)
        name.value = data.name
        positionId.value = data.positionId ?? null 
        const perms = await permissionStore.getUserPermissionById(props.user.id)
        selectedPermissions.value = perms.map(p => p.permissionId)
      } catch (error) {
        errorMessage.value = error.message
        showError.value = true;
      }
    }
    if (props.show && !props.user) {
      name.value = ""
      positionId.value = null
      selectedPermissions.value = []
    }
  }
)

const saveUser= async () => {
  try {
    if (props.user) {
    // Editar
    const data = {
      id: props.user?.id,
      name: name.value,
      positionId: positionId.value,
      permissions: selectedPermissions.value 
    }

    await permissionStore.updateUserPermission(data)
    emit("saved", data)
  } else {
    // Crear
    const data = {
      name: name.value,
      positionId: positionId.value,
      permissionIds: selectedPermissions.value 
    }
    emit("saved", data)
  }
  close()
  } catch (error) {
    errorMessage.value = error.message
    showError.value = true
  }
}

const close = () => {
  emit("close")
}
</script>


<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
      <div class="relative p-4 w-full max-w-lg max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">

          <!-- Header -->
          <div class="flex items-center justify-between p-4 border-b border-gray-200">
            <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
              {{ props.user? "Editar usuario" : "Crear usuario" }}
            </h3>
            <button type="button" @click="emit('close')" class="text-gray-400 hover:bg-gray-200 rounded-lg w-8 h-8 flex justify-center items-center">
              ✕
            </button>
          </div>

          <!-- Body -->
          <form class="p-4" @submit.prevent="saveUser">
            <div class="grid gap-4 mb-4">
              <div>
                <label class="block mb-2 text-sm font-medium">Nombre</label>
                <input v-model="name" type="text" class="w-full p-2.5 border rounded-lg" maxlength="20" required />
              </div>

              <div>
                <label class="block mb-2 text-sm font-medium">Asignar una posición</label>
                <select v-model="positionId" class="w-full p-2.5 border rounded-lg">
                  <option disabled value="">Seleccione una posición</option>
                  <option v-for="position in store.positions" :key="position.id" :value="position.id">
                    {{ position.name }}
                  </option>
                </select>
              </div>

              <!-- Permisos -->
              <div>
                <PermissionCheckBox v-model:selectedPermissions="selectedPermissions"/>
              </div>
            </div>

            <button type="submit" class="w-full text-white bg-blue-700 hover:bg-blue-800 rounded-lg px-5 py-2.5">
              {{ props.user ? "Actualizar" : "Agregar nuevo" }}
            </button>
          </form>

        </div>
      </div>
    </div>
  </teleport>

  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>
