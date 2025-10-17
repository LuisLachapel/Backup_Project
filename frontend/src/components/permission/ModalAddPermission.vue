<script setup>
import { ref, watch } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';
import ErrorModal from '../ErrorModal.vue';

const props = defineProps({
  show: { type: Boolean, default: false },
  permission: { type: Object, default: null }
});

const emit = defineEmits(['close', 'save'])
const store = usePermissionStore();

// Estados del formulario
const name = ref('');
const description = ref('');
const code = ref('');
const active = ref(false);

// Estados del modal de error
const showError = ref(false);
const errorMessage = ref("");

// Cargar datos si se está editando un permiso
watch(
  () => props.show,
  async () => {
    if (props.show && props.permission?.id) {
      try {
        const data = await store.getById(props.permission.id);
        name.value = data.name;
        code.value = data.code;
        description.value = data.description;
        active.value = data.active;
      } catch (error) {
        errorMessage.value = "Error al cargar el permiso.";
        showError.value = true;
      }
    }

    if (props.show && !props.permission) {
      name.value = "";
      description.value = "";
      code.value = "";
      active.value = false;
    }
  }
);

const close = () => {
  emit("close");
};

// Guardar (crear o editar)
const save = async () => {
  try {
    const data = {
      id: props.permission?.id,
      name: name.value,
      description: description.value,
      code: code.value,
      active: active.value
    };

    if (props.permission) {
      await store.update(data);
    
    } else {
      await store.insert(data);
    }

    close(); // cerrar el modal al completar
  } catch (error) {
    // Capturar errores del backend (400, validaciones, etc.)
    errorMessage.value = error.message;
    showError.value = true;
  }
};
</script>

<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
      <div class="relative p-4 w-full max-w-md max-h-full">
        <div class="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
          <!-- Header -->
          <div class="flex items-center justify-between p-4 border-b border-gray-200">
            <h3 class="text-lg font-semibold text-gray-900 dark:text-white">
              {{ props.permission ? "Editar permiso" : "Crear permiso" }}
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
          <form class="p-4" @submit.prevent="save">
            <div class="grid gap-4 mb-4">
              <div>
                <label class="block mb-2 text-sm font-medium">Nombre del permiso</label>
                <input
                  v-model="name"
                  type="text"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese el nombre"
                  maxlength="50"
                />
                <p class="text-sm text-gray-500">{{ name.length }}/50</p>
              </div>

              <div>
                <label class="block mb-2 text-sm font-medium">Código del permiso</label>
                <input
                  v-model="code"
                  type="text"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese el código"
                  maxlength="25"
                />
                <p class="text-sm text-gray-500">{{ code.length }}/25</p>
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
                <p class="text-sm text-gray-500">{{ description.length }}/100</p>
              </div>

              <div v-if="props.permission">
                <label class="block mb-2 text-sm font-medium">Estado</label>
                <label class="inline-flex items-center cursor-pointer">
                  <input type="checkbox" v-model="active" class="sr-only peer" />
                  <div
                    class="relative w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 
                    peer-focus:ring-blue-300 dark:peer-focus:ring-blue-800 rounded-full peer 
                    dark:bg-gray-700 peer-checked:after:translate-x-full 
                    rtl:peer-checked:after:-translate-x-full peer-checked:after:border-white 
                    after:content-[''] after:absolute after:top-[2px] after:start-[2px] 
                    after:bg-white after:border-gray-300 after:border after:rounded-full 
                    after:h-5 after:w-5 after:transition-all dark:border-gray-600 
                    peer-checked:bg-blue-600 dark:peer-checked:bg-blue-600"
                  ></div>
                  <span class="ms-3 text-sm font-medium text-gray-900 dark:text-gray-300">
                    {{ active ? "Activo" : "Inactivo" }}
                  </span>
                </label>
              </div>
            </div>

            <button
              type="submit"
              class="w-full text-white bg-blue-700 hover:bg-blue-800 rounded-lg px-5 py-2.5"
            >
              {{ props.permission ? "Actualizar" : "Agregar nuevo" }}
            </button>
          </form>
        </div>
      </div>
    </div>
  </teleport>

  <!-- Modal de error -->
  <ErrorModal :show="showError" :message="errorMessage" @close="showError = false" />
</template>

