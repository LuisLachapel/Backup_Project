<script setup>
import { onMounted } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';


const store = usePermissionStore()

onMounted(() =>{
     store.getPermissions();
})

</script>

<template>
<div class="p-4">
    <h2 class="text-xl font-semibold mb-4">Listado de permisos</h2>

    <table class="min-w-full border border-gray-300 text-sm text-left">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-4 py-2 border-b">Nombre</th>
          <th class="px-4 py-2 border-b">Codigo</th>
          <th class="px-4 py-2 border-b">Descripción</th>
          
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="permission in store.permissions"
          :key="permission.id"
          class="hover:bg-gray-50"
        >
          <td class="px-4 py-2 border-b">{{ permission.name }}</td>
          <td class="px-4 py-2 border-b">{{ permission.code }}</td>
          <td class="px-4 py-2 border-b">{{ permission.description }}</td>

          <!-- Botón Editar 
          <td class="px-4 py-2 border-b text-center">
            <EditPositionButton :position="position"/>
          </td>-->
        </tr>
      </tbody>
    </table>

    <p v-if="store.permissions.length === 0" class="text-gray-500 mt-4">
      No hay permisos disponibles.
    </p>
  </div>

</template>