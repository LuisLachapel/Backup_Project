<script setup>
import { onMounted, ref } from 'vue';
import { useUserStore } from '@/stores/userStore';
import EditUserButton from './EditUserButton.vue';
import DeleteUserButton from './DeleteUserButton.vue';

const store = useUserStore()


onMounted(async () => {
    try {
        await store.getAllUser()
    } catch (error) {
        
    }
})
</script>

<template>
 

    <div
    v-for="user in store.users"
    :key="user.id"
    class="max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow-sm "
  >
    <header class="flex items-center justify-between">
      <!-- Nombre -->
      <div class="flex items-center gap-2">
        <h5 class="text-2xl font-bold">{{ user.name}}</h5>
        <!-- Botón Editar -->
        <EditUserButton :user="user" /> 
      </div>

      <!-- Eliminar -->
      <DeleteUserButton :id="user.id" />
    </header>

    <p class="mb-2">{{ user.position }}</p>
   
  </div>


</template>