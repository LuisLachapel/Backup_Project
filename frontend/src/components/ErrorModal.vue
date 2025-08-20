<script setup>

defineProps({
  show: Boolean,
  message: String
});

</script>

<template>
  <teleport to="body">
    <div v-if="show" class="fixed inset-0 flex items-center justify-center bg-black/50 z-50">
      <div class="bg-white p-6 rounded-lg shadow-lg w-96">
        <h2 class="text-lg font-bold text-red-600 mb-4">Error</h2>
        <p class="text-gray-700">{{ message }}</p>
        <button 
          @click="$emit('close')" 
          class="mt-4 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
        >
          Cerrar
        </button>
      </div>
    </div>
  </teleport>
</template>
