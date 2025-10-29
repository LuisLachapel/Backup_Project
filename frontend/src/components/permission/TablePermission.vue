<script setup>
import { onMounted } from 'vue';
import { usePermissionStore } from '@/stores/permissionStore';
import EditPermissionButton from './EditPermissionButton.vue';


const store = usePermissionStore()

onMounted(async () => {
  await store.getPermissions();
})

</script>

<template>
  <div class="p-4">
    <h2 class="text-xl font-semibold mb-4">Listado de permisos</h2>

    <!-- Tabla (oculta en pantallas pequeñas) -->
    <div class="overflow-x-auto hidden sm:block rounded-lg border border-gray-300">
      <table class="min-w-full text-sm text-left ">
        <thead class="bg-gray-50">
          <tr>
            <th class="px-4 py-2 border-b">Nombre</th>
            <th class="px-4 py-2 border-b">Código</th>
            <th class="px-4 py-2 border-b">Descripción</th>
            <th class="px-4 py-2 border-b">Activo</th>
            <th class="px-4 py-2 border-b text-center">Editar</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="permission in store.permissions"
            :key="permission.id"
            class="bg-white border-b border-gray-400 hover:bg-gray-200"
          >
            <td class="px-4 py-2 border-b font-medium text-gray-900 whitespace-nowrap">{{ permission.name }}</td>
            <td class="px-4 py-2 border-b">{{ permission.code }}</td>
            <td class="px-4 py-2 border-b">{{ permission.description }}</td>
            <td class="px-4 py-2 border-b">
              <span
                class="px-2 py-1 rounded-full text-xs font-semibold"
                :class="permission.active ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'"
              >
                {{ permission.active ? "Activo" : "Inactivo" }}
              </span>
            </td>
            <td class="px-4 py-2 border-b text-center">
              <EditPermissionButton :permission="permission" />
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Vista tipo tarjetas (solo en móviles) -->
    <div class="block sm:hidden space-y-3">
      <div
        v-for="permission in store.permissions"
        :key="permission.id"
        class="border rounded-lg p-3 shadow-sm bg-white"
      >
        <div class="flex justify-between items-center">
          <p class="font-medium text-gray-700">{{ permission.name }}</p>
          <span
            class="px-2 py-1 rounded-full text-xs font-semibold"
            :class="permission.active ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'"
          >
            {{ permission.active ? "Activo" : "Inactivo" }}
          </span>
        </div>
        <p class="text-gray-500 text-sm mt-1">Código: {{ permission.code }}</p>
        <p class="text-gray-400 text-xs mt-1">{{ permission.description }}</p>

        <div class="mt-2 text-right">
          <EditPermissionButton :permission="permission" />
        </div>
      </div>
    </div>

    <p v-if="store.permissions.length === 0" class="text-gray-500 mt-4">
      No hay permisos disponibles.
    </p>
  </div>
</template>
