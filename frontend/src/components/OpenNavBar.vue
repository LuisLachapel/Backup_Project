<script setup>
import { defineEmits } from 'vue'

const emit = defineEmits(['toggle-sidebar'])
</script>

<template>
  <nav class="fixed top-0 left-0 z-50 w-full bg-white dark:bg-gray-900 border-b border-gray-200 dark:border-gray-700 flex items-center justify-between px-4 py-3 sm:px-6">
    <!-- Botón menú (solo visible en móvil) -->
    <button
      @click="$emit('toggle-sidebar')"
      class="sm:hidden inline-flex items-center justify-center p-2 rounded-md text-gray-700 hover:bg-gray-200 dark:text-gray-300 dark:hover:bg-gray-700"
    >
      <!-- Icono hamburguesa -->
      <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
              d="M4 6h16M4 12h16M4 18h16" />
      </svg>
    </button>

    <!-- Título o logo -->
    <h1 class="text-lg font-bold text-gray-800 dark:text-white">Backup System</h1>
  </nav>
</template>
