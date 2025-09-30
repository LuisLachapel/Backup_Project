<script setup>
import { ref, watch } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';

const props = defineProps({
    show: {type: Boolean, default: false},
    permission: {type: Object, default: null}
})

const emit = defineEmits(["close","save"]);
const store = usePermissionStore();
const name = ref('');
const description = ref('');
const code = ref('')

watch(
    () => props.show,
    async() => {
        if(props.show && props.permission?.id){
            try {
                //Aqui se obtendran los permisos con el metodo getById
            } catch (error) {
                
            }
        }
        if(props.show && !props.permission){
            name.value = "";
            description.value = "";
            code.value = "";
        }
    }
)

const close = () =>{
    emit("close")
}

const save = () =>{
    //Editar
    if(props.permission){
        //asignar los valores obtenidos
        const data ={
            id: props.permission.id,
            name: name.value,
            description: description.value,
            code: code.value
        }
        emit("save",data)
    }else{
        //crear
        const data = {
            name: name.value,
            description: description.value,
            code: code.value
        }
        emit("save",data)
    }
    close()
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
                  placeholder="Ingrese el título"
                  maxlength="50"
                />
                <p class="text-sm text-gray-500">{{ name.length }}/50</p>
              </div>

              <div>
                <label class="block mb-2 text-sm font-medium">codigo del permiso</label>
                <input
                  v-model="code"
                  type="text"
                  class="w-full p-2.5 border rounded-lg"
                  placeholder="Ingrese el codigo"
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
            </div>

            <button
              type="submit"
              class="w-full text-white bg-blue-700 hover:bg-blue-800 rounded-lg px-5 py-2.5"
            >
              {{ props.permission? "Actualizar" : "Agregar nuevo" }}
            </button>
          </form>
        </div>
      </div>
    </div>
  </teleport>

</template>