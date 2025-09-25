<script setup>
import { ref } from 'vue';
import { useSessionStore } from '@/stores/sessionStore';
import { useRouter } from 'vue-router';

const session = useSessionStore()
const router = useRouter()
const dropdownOpen = ref(false)

const toggleDropdown = () =>{
    dropdownOpen.value = !dropdownOpen.value
}

const logout = () =>{
    session.logout();
    router.push("/auth")
}

</script>


<template>
  <nav class="fixed top-0 left-0 right-0 h-14 bg-white dark:bg-gray-800 shadow flex items-center justify-between px-6 z-50">
    <!-- Título-->
    <h1 class="text-lg font-bold text-gray-700 dark:text-white">📒 Backup System</h1>

    <!-- Usuario y Dropdown -->
    <div class="relative">
      <button
        @mouseenter="toggleDropdown"
        
        class="flex items-center space-x-2 px-3 py-2 bg-gray-100 dark:bg-gray-700 rounded-lg hover:bg-gray-200 dark:hover:bg-gray-600"
      >
        <span class="text-gray-800 dark:text-white">
          {{ session.currentUser?.name || "Ingresar" }}
        </span>
        <svg
          class="w-4 h-4 text-gray-600 dark:text-gray-300"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          viewBox="0 0 24 24"
        >
          <path stroke-linecap="round" stroke-linejoin="round" d="M19 9l-7 7-7-7" />
        </svg>
      </button>

      <!-- Dropdown menu -->
      <div
        v-if="dropdownOpen"
        class="absolute right-0 mt-2 w-40 bg-white dark:bg-gray-700 rounded-lg shadow-lg py-2 z-50"
      >
        <button @mouseleave="toggleDropdown"
          @click="logout"
          class="w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-gray-100 dark:hover:bg-gray-600"
        >
          Cerrar sesión
        </button>
      </div>
    </div>
  </nav>
</template>