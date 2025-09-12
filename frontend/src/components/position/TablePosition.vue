<script setup>
import { onMounted } from 'vue';
import { usePositionStore } from '@/stores/positionStore';
import EditPositionButton from '@/components/position/EditPositionButton.vue'


const store = usePositionStore();

onMounted(() =>{
    store.getAllPosition()
})

</script>

<template>
  <div class="p-4">
    <h2 class="text-xl font-semibold mb-4">Listado de cargos</h2>

    <table class="min-w-full border border-gray-300 text-sm text-left">
      <thead class="bg-gray-100">
        <tr>
          <th class="px-4 py-2 border-b">Nombre</th>
          <th class="px-4 py-2 border-b">Descripción</th>
          <th class="px-4 py-2 border-b text-center">Editar</th>
          
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="position in store.positions"
          :key="position.id"
          class="hover:bg-gray-50"
        >
          <td class="px-4 py-2 border-b">{{ position.name }}</td>
          <td class="px-4 py-2 border-b">{{ position.description }}</td>

          <!-- Botón Editar -->
          <td class="px-4 py-2 border-b text-center">
            <EditPositionButton :position="position"/>
          </td>
        </tr>
      </tbody>
    </table>

    <p v-if="store.positions.length === 0" class="text-gray-500 mt-4">
      No hay cargos registrados.
    </p>
  </div>
</template>